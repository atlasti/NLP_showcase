//
//  Global.h
//  HelloWorldApp
//
//  Created by Markus Kirschner on 22.02.19.
//  Copyright © 2019 ATLAS.ti Scientific Software Development GmbH. All rights reserved.
//

#ifndef Global_h
#define Global_h

#define SSDWeakify(VAR) __weak __typeof__(VAR) _weak_##VAR = VAR
#define SSDStrongify(VAR) __strong __typeof__(VAR) VAR = _weak_##VAR

#endif /* Global_h */
