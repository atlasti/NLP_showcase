//
//  SSDLogger.h
//  ATLAS.ti
//
//  Created by Markus Kirschner on 17.09.18.
//  Copyright © 2018 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM(NSUInteger, SSDLogLevel) {
    SSDLogLevelVerbose,
    SSDLogLevelDebug,
    SSDLogLevelInfo,
    SSDLogLevelWarning,
    SSDLogLevelError,
    SSDLogLevelFatal,
    SSDLogLevelEvent
};

typedef NS_ENUM(NSUInteger, SSDLogOutput) {
    SSDLogOutputConsole,
    SSDLogOutputFile
};

typedef NS_OPTIONS(NSUInteger, SSDLogFormat) {
    SSDLogFormatDate = 1 << 0,
    SSDLogFormatTime = 1 << 1,
    SSDLogFormatThread = 1 << 2,
    SSDLogFormatSourceFileLine = 1 << 3,
    SSDLogFormatFunction = 1 << 4,
    SSDLogFormatShort = SSDLogFormatTime | SSDLogFormatThread | SSDLogFormatSourceFileLine,
    SSDLogFormatFull = SSDLogFormatDate | SSDLogFormatTime | SSDLogFormatThread | SSDLogFormatSourceFileLine | SSDLogFormatFunction,
    SSDLogFormatUnknown = 1 << 8
};

#define SSDLogWithLevel(LEVEL, MESSAGE) \
    if (LEVEL >= SSDLogger.sharedInstance.logLevel) { \
        [SSDLogger.sharedInstance logWithLevel:LEVEL file:__FILE__ function:__FUNCTION__ lineNumber:__LINE__ string:MESSAGE]; \
    }

#define SSDVerboseLog(...) SSDLogWithLevel(SSDLogLevelVerbose, ([NSString stringWithFormat:__VA_ARGS__]))
#define SSDDebugLog(...) SSDLogWithLevel(SSDLogLevelDebug, ([NSString stringWithFormat:__VA_ARGS__]))
#define SSDInfoLog(...) SSDLogWithLevel(SSDLogLevelInfo, ([NSString stringWithFormat:__VA_ARGS__]))
#define SSDWarnLog(...) SSDLogWithLevel(SSDLogLevelWarning, ([NSString stringWithFormat:__VA_ARGS__]))
#define SSDErrorLog(...) SSDLogWithLevel(SSDLogLevelError, ([NSString stringWithFormat:__VA_ARGS__]))
#define SSDFatalLog(...) SSDLogWithLevel(SSDLogLevelFatal, ([NSString stringWithFormat:__VA_ARGS__]))

#ifdef DEBUG
#define NSLog SSDDebugLog
#else
#define NSLog SSDInfoLog
#endif

// Compatibility
#define SSDLogError(error) \
    if (error) { \
        SSDErrorLog(@"Error occurred: %@(%li, %@)", error, error.code, error.userInfo); \
    }
#define SSDLog SSDInfoLog
#define SMLog SSDDebugLog
#define SMQuietLog SSDInfoLog

@interface SSDLogger : NSObject

+ (instancetype)sharedInstance;

@property (class, strong) NSURL *logFileFolderURL;

/***
 The default log level can be set with the environment variable SSD_LOG_LEVEL
 */
@property (nonatomic) SSDLogLevel logLevel;

/***
 Different parts of the log message can be turned on/off with this property (see definition).
 */
@property (nonatomic) SSDLogFormat logFormat;

/***
 Default is output to logfile, can be sent to the console by setting the environment variable SSD_LOG_TO_CONSOLE=1
 */
@property (nonatomic, readonly) SSDLogOutput outputTarget;

/***
 Only shows log messages where this regex matches the sourceFile in logWithLevel, (eg. "(SSD|SM)")
 default: nil, does not filter
 */
@property (nonatomic, copy, nullable) NSString *sourceFileFilter;

/***
 During startup the previous logfile will be overridden by the current one.
 */
@property (nonatomic, readonly) NSString *contentsOfCurrentLogFile;
@property (class, readonly) NSString *contentsOfPreviousLogFile;

/***
 Constructs a well-formed message out of the parameters and logs it to the console/file if the current logLevel matches
 */
- (void)logWithLevel:(SSDLogLevel)level file:(const char *)sourceFile function:(const char *)functionName lineNumber:(int)lineNumber string:(NSString *)string;

// Helper methods to translate between loglevel/logformat and human readable form
+ (NSString *)symbolForLogLevel:(SSDLogLevel)logLevel;

+ (NSString *)titleForLogLevel:(SSDLogLevel)logLevel;
+ (SSDLogLevel)logLevelForTitle:(NSString *)title;

+ (NSString *)titleForLogFormat:(SSDLogFormat)logFormat;
+ (SSDLogFormat)logFormatForTitle:(NSString *)title;

@end

NS_ASSUME_NONNULL_END
