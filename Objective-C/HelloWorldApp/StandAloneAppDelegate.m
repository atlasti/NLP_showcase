//
//  StandAloneAppDelegate.m
//  HelloWorldApp
//
//  Created by Markus Kirschner on 22.02.19.
//  Copyright © 2019 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import "StandAloneAppDelegate.h"

#import "Global.h"
#import "HelloWorldServerViewController.h"

#import <AppCenter/AppCenter.h>
#import <AppCenterAnalytics/AppCenterAnalytics.h>
#import <AppCenterCrashes/AppCenterCrashes.h>
#import <HelloWorldManager/HelloWorldManager.h>

@interface AppDelegate () <MSCrashesDelegate>

@end

@implementation StandAloneAppDelegate

- (IBAction)crashTest:(id)sender {
    [MSCrashes generateTestCrash];
}

- (void)applicationDidFinishLaunching:(NSNotification *)notification {
    [[NSUserDefaults standardUserDefaults] registerDefaults:@{@"NSApplicationCrashOnExceptions": @YES}];
    [MSCrashes setDelegate:self];
    [MSAppCenter start:@"b2791c01-1ebc-448b-b4f7-33c17f31c9f8" withServices:@ [[MSAnalytics class], [MSCrashes class]]];

    [MSAnalytics trackEvent:@"Connecting to External Server"];
    [HelloWorldManager.sharedManager startWithCompletionBlock:^{
        [self serverReady];
    }];
}

- (void)serverReady {
    [super serverReady];

    [HelloWorldServerViewController showWindow];
}

- (void)applicationWillTerminate:(NSNotification *)notification {
    [HelloWorldManager.sharedManager.textServer shutdown];
}

@end
