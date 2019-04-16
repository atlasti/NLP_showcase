//
//  Created by Markus Kirschner on 22.02.19.
//  Copyright © 2019 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import "HelloWorldServer.h"

#import <Cocoa/Cocoa.h>

typedef void (^HelloWorldManagerCompletionBlock)(void);

@interface HelloWorldManager : NSObject

- (instancetype _Nullable)init NS_UNAVAILABLE;  // This is a Singleton, use sharedManager instead

+ (HelloWorldManager *_Nonnull)sharedManager;

/**
 @brief Starts the HelloWorldManager and all its internal components
 @param completionBlock will be called when everything is set up properly
*/
- (void)startWithCompletionBlock:(HelloWorldManagerCompletionBlock _Nonnull)completionBlock;

/**
 @brief Returns the HelloWorldServer after startWithCompletionBlock has been called
 */
@property (nonatomic, strong, readonly) HelloWorldServer *_Nullable server;

- (void)sayHelloAgainWithName:(NSString *_Nonnull)name completionBlock:(void (^_Nonnull)(float, NSString *_Nullable))completionBlock;

- (void)sayHelloWithName:(NSString *_Nonnull)name completionBlock:(void (^_Nonnull)(NSString *_Nullable))completionBlock;

@end
