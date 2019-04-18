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

//TODO:SP make another action that does lemma search
- (IBAction)helloAgainAction:(id)sender {
    [HelloWorldManager.sharedManager sayHelloAgainWithName:@"/Users/atlasti/test_document.txt -NER"
                                           completionBlock:^(float percentage, NSString *_Nullable message) {
                                               //TODO:SP do other stuff
                                               //make lists out of NER stuff
                                               //parse response string
                                               NSArray<NSString *> *components = [message componentsSeparatedByString:@"\n"];
                                               if (components.count > 1) {
                                                   NSMutableArray<NSNumber *> *startPositions = [[NSMutableArray<NSNumber *> alloc] init];
                                                   NSMutableArray<NSNumber *> *endPositions = [[NSMutableArray<NSNumber *> alloc] init];
                                                   NSMutableArray<NSString *> *NERTypes = [[NSMutableArray<NSString *> alloc] init];
                                                   SSDInfoLog(@"******** Start of Message ********");
                                                   NSUInteger numberOfComponents = components.count;
                                                   NSUInteger NERIndex = 0;
                                                   for (int index = 0; index + 2 < numberOfComponents; index = index + 3) {
                                                       SSDInfoLog(@"WTF %@", components[index]);
                                                       startPositions[NERIndex] = [NSNumber numberWithInteger:components[index].integerValue];
                                                       endPositions[NERIndex] = [NSNumber numberWithInteger:components[index + 1].integerValue];
                                                       NERTypes[NERIndex] = components[index + 2];
                                                       NERIndex++;
                                                   }
                                                   SSDInfoLog(@"******** End of Message ********");
                                               }
                                           }];
}

@end
