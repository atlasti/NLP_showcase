//
//  Created by Markus Kirschner on 25.02.19.
//  Copyright © 2019 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface HelloWorldServer : NSObject

@property (nonatomic, strong, readonly) NSMutableString *standardOutput;
@property (nonatomic, strong, readonly) NSMutableString *standardError;

- (void)shutdown;

@end
