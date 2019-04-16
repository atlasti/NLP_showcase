# Setup

After first checkout please enter this command in bash, which will enable git hooks:

    git submodule update --init --recursive

You will also need to install the following components

# Homebrew

    /usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"

# Protocol buffers

    brew install grpc

# Cocoapods

    sudo gem install cocoapods
    
If something goes wrong with the system ruby you should consider install a more recent version with rbenv and install the gem without sudo.

    brew install rbenv
    rbenv init
    curl -fsSL https://github.com/rbenv/rbenv-installer/raw/master/bin/rbenv-doctor | bash
    
# More ruby gems

The Rakefile has some dependencies on these rubygems

    gem install ci_reporter_test_unit xcpretty
    
# Setup project

Enter this command from the root project folder:

    rake prepare

# Structure

HelloWorld.sln - Solution including the HelloWorldServer (C# component)

-> Start HelloWorldServer with Visual Studio (or Jetbrains Rider)

or start server from commandline:

    ./HelloWorldServer/bin/Release/netcoreapp2.2/osx-x64/publish/HelloWorldServer

ObjC/HelloWorldManager.xcworkspace

-> Start HelloWorldAppTester