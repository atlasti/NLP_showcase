//
//  SSDLogger.m
//  ATLAS.ti
//
//  Created by Markus Kirschner on 17.09.18.
//  Copyright © 2018 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import "SSDLogger.h"

#include <sys/stat.h>

static NSString *kLogFormatKey = @"SSDLogger.logFormat";
static NSString *const SSDLogFileName = @"logging.txt";
static NSString *const SSDPreviousLogFileName = @"previous_logging.txt";

static SSDLogger *s_sharedInstance;

@interface SSDLogger ()
@property (nonatomic, readwrite) SSDLogOutput outputTarget;

@end

@implementation SSDLogger {
    dispatch_queue_t outputQueue;
    SSDLogFormat _logFormat;
}

+ (void)initialize {
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        s_sharedInstance = [[self alloc] init];
    });
}

+ (instancetype)sharedInstance {
    return s_sharedInstance;
}

- (instancetype)init {
    self = [super init];
    if (self != nil) {
        _logFormat = SSDLogFormatUnknown;
        self.outputTarget = SSDLogger.defaultOutput;

        outputQueue = dispatch_queue_create("com.atlas-ti.SSDLogger", DISPATCH_QUEUE_SERIAL);

        if (self.outputTarget == SSDLogOutputFile) {
            [self redirectToLogFile];
        }
        _logLevel = -1;
        self.logLevel = SSDLogger.defaultLogLevel;
    }

    return self;
}

#pragma mark - Properties

- (void)setLogLevel:(SSDLogLevel)logLevel {
    if (_logLevel == logLevel) {
        return;
    }
    _logLevel = logLevel;
    NSString *message = [NSString stringWithFormat:@"🔧 Changed minimum log level to %@", [SSDLogger titleForLogLevel:logLevel]];
    [self logWithLevel:logLevel file:__FILE__ function:__FUNCTION__ lineNumber:__LINE__ string:message];
}

- (SSDLogFormat)logFormat {
    if (_logFormat == SSDLogFormatUnknown) {
        BOOL logFormatMissingInUserDefaults = [NSUserDefaults.standardUserDefaults stringForKey:kLogFormatKey] == nil;
        if (logFormatMissingInUserDefaults) {
            _logFormat = [SSDLogger defaultLogFormatForOutputTarget:self.outputTarget];
        }
        else {
            _logFormat = [NSUserDefaults.standardUserDefaults integerForKey:kLogFormatKey];
        }
    }
    return _logFormat;
}

- (void)setLogFormat:(SSDLogFormat)logFormat {
    if (_logFormat == logFormat) {
        return;
    }
    _logFormat = logFormat;
    [NSUserDefaults.standardUserDefaults setInteger:_logFormat forKey:kLogFormatKey];
    [self logWithLevel:_logLevel file:__FILE__ function:__FUNCTION__ lineNumber:__LINE__ string:@"🔧 Changed log format"];
}

- (void)setSourceFileFilter:(NSString *)sourceFileFilter {
    if ([_sourceFileFilter isEqualToString:sourceFileFilter]) {
        return;
    }
    if (sourceFileFilter.length > 0) {
        NSError *error = nil;
        NSRegularExpression *regex = [NSRegularExpression regularExpressionWithPattern:sourceFileFilter options:NSRegularExpressionCaseInsensitive error:&error];
        if (regex) {
            _sourceFileFilter = sourceFileFilter;
        }
        else {
            NSString *message = [NSString stringWithFormat:@"🔧 Ignoring invalid regex '%@', reason:%@", sourceFileFilter, error];
            _sourceFileFilter = nil;
            [self logWithLevel:SSDLogLevelError file:__FILE__ function:__FUNCTION__ lineNumber:__LINE__ string:message];
            return;
        }
    }
    else {
        _sourceFileFilter = nil;
    }
    NSString *message = [NSString stringWithFormat:@"🔧 Changed classFilterRegex to %@", _sourceFileFilter ?: @"nil"];
    [self logWithLevel:self.logLevel file:__FILE__ function:__FUNCTION__ lineNumber:__LINE__ string:message];
}

+ (NSURL *)logFileFolderURL {
    NSURL *logFileFolderURL = [[[NSFileManager new] URLsForDirectory:NSApplicationSupportDirectory inDomains:NSUserDomainMask] lastObject];
    return logFileFolderURL;
}

+ (NSString *)contentsOfPreviousLogFile {
    return [NSString stringWithContentsOfURL:[SSDLogger.logFileFolderURL URLByAppendingPathComponent:SSDPreviousLogFileName] encoding:NSUTF8StringEncoding error:nil];
}

- (NSString *)contentsOfCurrentLogFile {
    // Prevent clutter by synchronizing with outputQueue
    __block NSString *contents = nil;
    dispatch_sync(outputQueue, ^{
        contents = [NSString stringWithContentsOfURL:[SSDLogger.logFileFolderURL URLByAppendingPathComponent:SSDLogFileName] encoding:NSUTF8StringEncoding error:nil];
    });
    return contents;
}

#pragma mark - Public Methods

- (void)logWithLevel:(SSDLogLevel)level file:(const char *)sourceFile function:(const char *)functionName lineNumber:(int)lineNumber string:(NSString *)string {
    if (level < self.logLevel) {
        return;
    }
    NSString *sourceName = [SSDLogger sourceNameFor:sourceFile];
    if (![sourceName isEqualToString:@"SSDLogger.m"] &&
        self.sourceFileFilter &&
        [sourceName rangeOfString:self.sourceFileFilter
                          options:NSRegularExpressionSearch]
                .location == NSNotFound) {
        return;
    }

    time_t now = time(NULL);
    CFAbsoluteTime floatTime = CFAbsoluteTimeGetCurrent();
    int threadNumber = self.currentThreadNumber;
    SSDLogFormat logFormat = self.logFormat;

    dispatch_sync(outputQueue, ^{
        NSMutableString *output = [NSMutableString new];
        char cStringBuffer[16];

        if (logFormat & SSDLogFormatDate) {
            strftime(cStringBuffer, sizeof(cStringBuffer), "%y-%m-%d", localtime(&now));
            [output appendFormat:@"%s ", cStringBuffer];
        }
        if (logFormat & SSDLogFormatTime) {
            strftime(cStringBuffer, sizeof(cStringBuffer), "%H:%M:%S", localtime(&now));

            int milliseconds = (floatTime - floor(floatTime)) * 1000;
            [output appendFormat:@"%s,%03d", cStringBuffer, milliseconds];
        }

        [output appendFormat:@"%@ ", [SSDLogger symbolForLogLevel:level]];

        if (logFormat & SSDLogFormatThread) {
            [output appendFormat:@"~%d ", threadNumber];
        }

        if (logFormat & SSDLogFormatSourceFileLine) {
            [output appendFormat:@"[%@:%d] ", sourceName, lineNumber];
        }

        if (logFormat & SSDLogFormatFunction) {
            [output appendFormat:@"%s ", functionName];
        }

        NSString *anonymizedString = [string stringByReplacingOccurrencesOfString:NSHomeDirectory() withString:@"~"];
        [output appendFormat:@"%@\n", anonymizedString];

        const char *message = [output UTF8String];
        fwrite(message, 1, strlen(message), stderr);
        fflush(stderr);
    });
}

#pragma mark - Helpers

+ (NSString *)sourceNameFor:(const char *)sourceFile {
    NSString *sourceName = [[[NSString alloc] initWithBytes:sourceFile length:strlen(sourceFile) encoding:NSUTF8StringEncoding] lastPathComponent];
    return sourceName;
}

- (int)currentThreadNumber {
    if ([NSThread isMainThread]) {
        return 1;
    }
    static NSString *currentThreadNumberKey = @"SSDLogger.currentThreadNumberKey";
    NSNumber *threadNumber = NSThread.currentThread.threadDictionary[currentThreadNumberKey];
    if (threadNumber != nil) {
        return [threadNumber intValue];
    }

    const char *threadString = [[[NSThread currentThread] description] UTF8String];
    const char *patterns[] = {"number = ", "num = "};
    for (int patternNo = 0, patternMax = (sizeof(patterns) / sizeof(char *)); patternNo < patternMax; patternNo++) {
        const char *pattern = patterns[patternNo];
        char *numLocation = strstr(threadString, pattern);
        if (numLocation != NULL) {
            numLocation += strlen(pattern);
            threadNumber = @(atoi(numLocation));
            NSThread.currentThread.threadDictionary[currentThreadNumberKey] = threadNumber;
            return [threadNumber intValue];
        }
    }
    return -1;
}

#pragma mark - Log level

+ (NSString *)symbolForLogLevel:(SSDLogLevel)logLevel {
    switch (logLevel) {
        case SSDLogLevelVerbose:
            return @"💭";
            break;
        case SSDLogLevelDebug:
            return @"🐞";
            break;
        case SSDLogLevelInfo:
            return @"ℹ️";
            break;
        case SSDLogLevelWarning:
            return @"⚠️";
            break;
        case SSDLogLevelError:
            return @"❌";
            break;
        case SSDLogLevelFatal:
            return @"😱";
            break;
        case SSDLogLevelEvent:
            return @"🚦";
            break;
    }
}

+ (NSString *)titleForLogLevel:(SSDLogLevel)logLevel {
    switch (logLevel) {
        case SSDLogLevelVerbose:
            return @"Verbose";
            break;
        case SSDLogLevelDebug:
            return @"Debug";
            break;
        case SSDLogLevelInfo:
            return @"Info";
            break;
        case SSDLogLevelWarning:
            return @"Warning";
            break;
        case SSDLogLevelError:
            return @"Error";
            break;
        case SSDLogLevelFatal:
            return @"Fatal";
            break;
        case SSDLogLevelEvent:
            return @"Event";
            break;
    }
}

+ (SSDLogLevel)logLevelForTitle:(NSString *)title {
    SSDLogLevel logLevel = SSDLogLevelInfo;
    if ([title hasPrefix:@"V"]) {
        logLevel = SSDLogLevelVerbose;
    }
    else if ([title hasPrefix:@"D"]) {
        logLevel = SSDLogLevelDebug;
    }
    else if ([title hasPrefix:@"I"]) {
        logLevel = SSDLogLevelInfo;
    }
    else if ([title hasPrefix:@"W"]) {
        logLevel = SSDLogLevelWarning;
    }
    else if ([title hasPrefix:@"E"]) {
        logLevel = SSDLogLevelError;
    }
    else if ([title hasPrefix:@"F"]) {
        logLevel = SSDLogLevelFatal;
    }

    return logLevel;
}

+ (SSDLogLevel)defaultLogLevel {
    NSString *logLevelString = [NSProcessInfo.processInfo.environment[@"SSD_LOG_LEVEL"] uppercaseString];
    return [self logLevelForTitle:logLevelString];
}

#pragma mark - LogFormat

+ (NSString *)titleForLogFormat:(SSDLogFormat)logFormat {
    if (logFormat & SSDLogFormatDate) {
        return @"Date";
    }
    if (logFormat & SSDLogFormatTime) {
        return @"Time";
    }
    if (logFormat & SSDLogFormatThread) {
        return @"Thread";
    }
    if (logFormat & SSDLogFormatSourceFileLine) {
        return @"Sourcefile & Linenumber";
    }
    if (logFormat & SSDLogFormatFunction) {
        return @"Function";
    }
    return @"Unsupported";
}

+ (SSDLogFormat)logFormatForTitle:(NSString *)title {
    if ([title hasPrefix:@"D"]) {
        return SSDLogFormatDate;
    }
    if ([title hasPrefix:@"Ti"]) {
        return SSDLogFormatTime;
    }
    if ([title hasPrefix:@"Th"]) {
        return SSDLogFormatThread;
    }
    if ([title hasPrefix:@"S"]) {
        return SSDLogFormatSourceFileLine;
    }
    if ([title hasPrefix:@"F"]) {
        return SSDLogFormatFunction;
    }
    return SSDLogFormatUnknown;
}

+ (SSDLogFormat)defaultLogFormatForOutputTarget:(SSDLogOutput)outputTarget {
    return (outputTarget == SSDLogOutputFile ? SSDLogFormatFull : SSDLogFormatShort);
}

#pragma mark - Output

+ (SSDLogOutput)defaultOutput {
    BOOL logToConsole = [NSProcessInfo.processInfo.environment[@"SSD_LOG_TO_CONSOLE"] boolValue];
    return logToConsole ? SSDLogOutputConsole : SSDLogOutputFile;
}

- (void)redirectToLogFile {
    NSString *logFile = [SSDLogger.logFileFolderURL URLByAppendingPathComponent:SSDLogFileName].path;
    NSFileManager *fileManager = [NSFileManager new];
    if ([fileManager fileExistsAtPath:logFile]) {
        NSString *previousLogFile = [SSDLogger.logFileFolderURL URLByAppendingPathComponent:SSDPreviousLogFileName].path;
        ;
        if ([fileManager fileExistsAtPath:previousLogFile]) {
            [fileManager removeItemAtPath:previousLogFile error:nil];
        }

        [fileManager moveItemAtPath:logFile toPath:previousLogFile error:nil];
    }
    umask(022);
    freopen([logFile UTF8String], "a", stderr);
}

@end
