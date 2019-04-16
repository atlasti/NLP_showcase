//
//  Created by Markus Kirschner on 25.02.19.
//  Copyright © 2019 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import "HelloWorldServer.h"

typedef void (^HelloWorldServerCompletionBlock)(void);

@interface HelloWorldServer (Private)

- (instancetype)initWithPort:(NSInteger)port completionBlock:(HelloWorldServerCompletionBlock)completionBlock;

@end
