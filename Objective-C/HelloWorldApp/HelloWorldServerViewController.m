//  HelloWorldApp
//
//  Created by Markus Kirschner on 08.02.19.
//  Copyright © 2019 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import "HelloWorldServerViewController.h"

#import <HelloWorldManager/HelloWorldManager.h>

@interface HelloWorldServerViewController ()

@property (nonatomic, strong) IBOutlet NSTextView *outputView;
@property (nonatomic, strong) IBOutlet NSTextView *errorView;
@property (nonatomic, strong) ATextServer *textServer;

@end

@implementation HelloWorldServerViewController

static int kATextServerStandardOutputKVOContext;
static int kATextServerStandardErrorKVOContext;

+ (void)showWindow {
    HelloWorldServerViewController *textServerViewController = [[HelloWorldServerViewController alloc] initWithNibName:@"SSDTextServer" bundle:nil];
    NSWindow *consoleWindow = [NSWindow windowWithContentViewController:textServerViewController];
    consoleWindow.contentViewController = textServerViewController;
    [consoleWindow makeKeyAndOrderFront:self];
}

- (void)viewWillAppear {
    [super viewWillAppear];

    self.textServer = HelloWorldManager.sharedManager.textServer;
}

- (void)viewDidAppear {
    [super viewDidAppear];
    [self updateTextView:self.outputView withOutput:self.textServer.standardOutput color:NSColor.textColor];
    [self updateTextView:self.errorView withOutput:self.textServer.standardError color:NSColor.redColor];
}

- (void)viewWillDisappear {
    [super viewWillDisappear];

    self.textServer = nil;
}

- (void)setTextServer:(ATextServer *)textServer {
    if (_textServer == textServer) {
        return;
    }
    if (_textServer) {
        [_textServer removeObserver:self forKeyPath:@"standardOutput" context:&kATextServerStandardOutputKVOContext];
        [_textServer removeObserver:self forKeyPath:@"standardError" context:&kATextServerStandardErrorKVOContext];
    }
    _textServer = textServer;
    if (_textServer) {
        [_textServer addObserver:self forKeyPath:@"standardOutput" options:0 context:&kATextServerStandardOutputKVOContext];
        [_textServer addObserver:self forKeyPath:@"standardError" options:0 context:&kATextServerStandardErrorKVOContext];
    }
}

- (void)observeValueForKeyPath:(NSString *)keyPath ofObject:(id)object change:(NSDictionary<NSKeyValueChangeKey, id> *)change context:(void *)context {
    if (context == &kATextServerStandardOutputKVOContext) {
        [self updateTextView:self.outputView withOutput:self.textServer.standardOutput color:NSColor.textColor];
    }
    else if (context == &kATextServerStandardErrorKVOContext) {
        [self updateTextView:self.errorView withOutput:self.textServer.standardError color:NSColor.redColor];
    }
    else {
        [super observeValueForKeyPath:keyPath ofObject:object change:change context:context];
    }
}

- (void)updateTextView:(NSTextView *)textView withOutput:(NSString *)output color:(NSColor *)color {
    dispatch_async(dispatch_get_main_queue(), ^{
        NSDictionary *attributes = @{NSFontAttributeName: [NSFont fontWithName:@"Menlo" size:12.0], NSForegroundColorAttributeName: color};
        NSInteger insertionPoint = textView.textStorage.length;
        NSString *newOutput = [output substringFromIndex:insertionPoint];
        [[textView textStorage] appendAttributedString:[[NSAttributedString alloc] initWithString:newOutput attributes:attributes]];
        [textView scrollRangeToVisible:NSMakeRange([textView.string length], 0)];
    });
}

@end
