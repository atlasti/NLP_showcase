//
//  Created by Markus Kirschner on 21.02.19.
//  Copyright © 2019 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import "HelloWorldManager.h"

#import "Global.h"
#import "HelloWorldServer_Private.h"

#import <GRPCClient/GRPCCall+ChannelArg.h>
#import <GRPCClient/GRPCCall+Tests.h>
#import <HelloWorldManager/HelloWorld.pbrpc.h>

static NSString *const kServer = @"localhost";
static NSInteger kDefaultServerPort = 50052;

@interface HelloWorldManager ()

@property (nonatomic) NSInteger serverPort;

@property (nonatomic, strong, readwrite) HelloWorldServer *server;
@property (nonatomic, strong) SSDHelloWorldService *client;

@property (nonatomic, copy) HelloWorldManagerCompletionBlock completionBlock;

@end

@implementation HelloWorldManager

static HelloWorldManager *g_singletonInstance = nil;
+ (HelloWorldManager *)sharedManager {
    // Singleton pattern
    static dispatch_once_t dispatchOnceToken;
    dispatch_once(&dispatchOnceToken, ^{ g_singletonInstance = [HelloWorldManager new]; });
    return g_singletonInstance;
}

- (void)startWithCompletionBlock:(HelloWorldManagerCompletionBlock)completionBlock {
    self.completionBlock = completionBlock;
    NSNumber *externalServerPort = NSBundle.mainBundle.infoDictionary[@"HelloWorldManagerConnectToExternalTextServerPort"];
    if (externalServerPort == nil) {
        SSDInfoLog(@"Starting embedded server on port %ld", kDefaultServerPort);
        self.server = [[HelloWorldServer alloc] initWithPort:kDefaultServerPort
                                             completionBlock:^{
                                                 [self startClientWithPort:kDefaultServerPort];
                                             }];
    }
    else {
        SSDInfoLog(@"Connecting to external server on port %@", externalServerPort);
        [self startClientWithPort:externalServerPort.integerValue];
    }
}

- (void)startClientWithPort:(NSInteger)serverPort {
    self.serverPort = serverPort;

    NSString *serverAddress = [NSString stringWithFormat:@"%@:%ld", kServer, serverPort];
    [GRPCCall useInsecureConnectionsForHost:serverAddress];
    [GRPCCall setUserAgentPrefix:@"HelloWorld/1.0" forHost:serverAddress];
    [GRPCCall setResponseSizeLimit:UINT_MAX forHost:serverAddress];
    self.client = [[SSDHelloWorldService alloc] initWithHost:serverAddress];

    HelloWorldManagerCompletionBlock completionBlock = self.completionBlock;
    self.completionBlock = nil;
    completionBlock();
}

- (void)showErrorAlert:(NSError *)error {
    SSDWarnLog(@"Failed: %@", error);
    NSAlert *alert = [NSAlert new];
    [alert addButtonWithTitle:@"Dismiss"];
    alert.messageText = [NSString stringWithFormat:@"Could not connect to %@%.ld", kServer, self.serverPort];
    alert.informativeText = error != nil ? [error description] : @"Unknown error";
    alert.alertStyle = NSAlertStyleCritical;
    [alert runModal];
}

- (void)sayHelloAgainWithName:(NSString *_Nonnull)name completionBlock:(void (^_Nonnull)(float, NSString *_Nullable))completionBlock {
    SSDSayHelloAgainActionRequest *request = [SSDSayHelloAgainActionRequest message];
    request.name = name;

    [self.client sayHelloAgainWithRequest:request
                             eventHandler:^(BOOL done, SSDSayHelloAgainActionReply *_Nullable response, NSError *_Nullable error) {
                                 if (done) {
                                     return;
                                 }
                                 if (response == nil || error != nil) {
                                     [self showErrorAlert:error];
                                     return;
                                 }

                                 completionBlock(response.percentageComplete, response.helloWorld.message);
                             }];
}

- (void)sayHelloWithName:(NSString *_Nonnull)name completionBlock:(void (^_Nonnull)(NSString *_Nullable))completionBlock {
    SSDSayHelloActionRequest *request = [SSDSayHelloActionRequest message];
    request.name = name;

    [self.client sayHelloWithRequest:request
                             handler:^(SSDSayHelloActionReply *_Nullable response, NSError *_Nullable error) {
                                 if (error == nil) {
                                     completionBlock(response.helloWorld.message);
                                 }
                                 else {
                                     [self showErrorAlert:error];
                                     completionBlock(nil);
                                 }
                             }];
}

@end
