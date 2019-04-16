//
//  Created by Markus Kirschner on 25.02.19.
//  Copyright © 2019 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import "Global.h"
#import "HelloWorldServer_Private.h"

@interface HelloWorldServer ()

@property (nonatomic) NSInteger port;

@property (nonatomic, strong) NSTask *externalTask;
@property (nonatomic, strong) NSFileHandle *outputFileHandle;
@property (nonatomic, strong) NSFileHandle *errorOutputFileHandle;

@property (nonatomic, strong, readwrite) NSMutableString *standardOutput;
@property (nonatomic, strong, readwrite) NSMutableString *standardError;

@property (nonatomic, copy) HelloWorldServerCompletionBlock completionBlock;

@property (nonatomic) BOOL shuttingDown;

@end

@implementation HelloWorldServer

- (instancetype)initWithPort:(NSInteger)port completionBlock:(HelloWorldServerCompletionBlock)completionBlock {
    self = [self init];
    if (self != nil) {
        self.completionBlock = completionBlock;
        self.port = port;
        self.standardOutput = [NSMutableString new];
        self.standardError = [NSMutableString new];
        [self start];
    }
    return self;
}

- (void)start {
    system("killall SSDHelloWorldServer");

    self.externalTask = [NSTask new];
    self.externalTask.launchPath = [NSBundle.mainBundle.privateFrameworksPath stringByAppendingString:@"/HelloWorldManager.framework/Resources/publish/SSDHelloWorldServer"];
    self.externalTask.arguments = @[[NSString stringWithFormat:@"%ld", self.port]];

    NSString *fontCache = [[NSSearchPathForDirectoriesInDomains(NSCachesDirectory, NSUserDomainMask, YES) firstObject] stringByAppendingPathComponent:NSBundle.mainBundle.infoDictionary[@"CFBundleIdentifier"]];
    self.externalTask.environment = @{@"XDG_CACHE_HOME": fontCache, @"FONTCONFIG_PATH": [NSBundle.mainBundle resourcePath]};

    NSPipe *outputPipe = [NSPipe pipe];
    self.externalTask.standardOutput = outputPipe;
    NSPipe *errorPipe = [NSPipe pipe];
    self.externalTask.standardError = errorPipe;

    self.outputFileHandle = [outputPipe fileHandleForReading];
    SSDWeakify(self);
    self.outputFileHandle.readabilityHandler = ^(NSFileHandle *fileHandle) {
        SSDStrongify(self);
        if (self.completionBlock != NULL) {
            [self serverStarted];
        }
        [self.standardOutput appendString:[[NSString alloc] initWithData:fileHandle.availableData encoding:NSUTF8StringEncoding]];
    };
    self.errorOutputFileHandle = [errorPipe fileHandleForReading];
    self.errorOutputFileHandle.readabilityHandler = ^(NSFileHandle *fileHandle) {
        SSDStrongify(self);
        if (self.completionBlock != NULL) {
            [self serverStarted];
        }
        [self.standardError appendString:[[NSString alloc] initWithData:fileHandle.availableData encoding:NSUTF8StringEncoding]];
    };

    [NSNotificationCenter.defaultCenter addObserver:self
                                           selector:@selector(taskTerminated:)
                                               name:NSTaskDidTerminateNotification
                                             object:self.externalTask];

    [self.externalTask launch];
}

- (void)serverStarted {
    HelloWorldServerCompletionBlock completionBlock = self.completionBlock;
    self.completionBlock = NULL;
    dispatch_async(dispatch_get_main_queue(), ^{
        completionBlock();
    });
}

- (void)shutdown {
    self.shuttingDown = YES;
    [self.externalTask terminate];
}

#pragma mark -
#pragma mark notification handler methods

- (void)taskTerminated:(NSNotification *)notification {
    if (self.shuttingDown) {
        // Unexpected shutdown
        [self.standardError appendFormat:@"\n🧨 Unexpected termination of text server.\n"];
    }
    [NSNotificationCenter.defaultCenter removeObserver:self
                                                  name:NSTaskDidTerminateNotification
                                                object:self.externalTask];

    [self.outputFileHandle closeFile];
    self.outputFileHandle = nil;
    [self.errorOutputFileHandle closeFile];
    self.errorOutputFileHandle = nil;
    self.externalTask = nil;
    self.shuttingDown = NO;
}

@end
