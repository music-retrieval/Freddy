# Freddy

## Build and Run

    docker build -t freddy .
    docker run freddy

Note that this dockerized has 3 stages:

1. Pull an Essentia image and compile C++ code
1. Pull a .NET developement image and build C# project
1. Compile both togther (this doesn't yet work)

Issue is most likely an OS mismatch problem (dll vs so file)
or just a long tree of dependencies.

Good luck.