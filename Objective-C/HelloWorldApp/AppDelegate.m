//
//  Created by Markus Kirschner on 09.01.19.
//  Copyright © 2019 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import "AppDelegate.h"

#import "SSDLogger.h"

#import "Global.h"
#import "MyWindowController.h"

#import <HelloWorldManager/HelloWorldManager.h>

static BOOL _serverRunning = NO;

@interface AppDelegate ()

@property (nonatomic, strong) id startUpObserver;
@property (nonatomic, strong) MyWindowController *windowController;

@end

@implementation AppDelegate

- (void)applicationDidFinishLaunching:(NSNotification *)notification {
    [HelloWorldManager.sharedManager startWithCompletionBlock:^{
        [self serverReady];
    }];
}

- (void)serverReady {
    dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(0.5 * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
        SSDInfoLog(@"Server ready.");
        _serverRunning = YES;
        self.windowController = [[MyWindowController alloc] initWithWindowNibName:@"MyWindowController"];
        [self.windowController showWindow:self.windowController.window];
    });
}

- (void)applicationWillTerminate:(NSNotification *)notification {
    _serverRunning = NO;
}

@end
