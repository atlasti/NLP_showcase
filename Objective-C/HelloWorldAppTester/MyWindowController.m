//
//  MyWindowController.m
//  HelloWorldAppTester
//
//  Created by Markus Kirschner on 20.03.19.
//  Copyright © 2019 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import "MyWindowController.h"

#import "SSDLogger.h"

#import <HelloWorldManager/HelloWorldManager.h>

@interface MyWindowController ()

@property (strong) IBOutlet NSTextView *textView;

@end

@implementation MyWindowController

- (void)windowDidLoad {
    [super windowDidLoad];

    [HelloWorldManager.sharedManager sayHelloWithName:@"World"
                                      completionBlock:^(NSString *_Nullable message) {
                                          SSDInfoLog(@"Returned: %@", message);
                                      }];
}

- (IBAction)helloAgainAction:(id)sender {
    [HelloWorldManager.sharedManager sayHelloAgainWithName:@"Welt"
                                           completionBlock:^(float percentage, NSString *_Nullable message) {
                                               SSDInfoLog(@"%.2f %%, %@", percentage, message);
                                           }];
}

@end
