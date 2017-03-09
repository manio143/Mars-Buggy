# Mars-Buggy
A clone of a simple game Moon-Buggy, where you're driving a small vehicle and have to jump over craters. After cloning basic functionality I might add some new feaures.

This projects is my first a bit more advanced game that I intend to complete. I'm writing in F# using SadConsole on top of MonoGame framework.

I also started with this project in a competition "Get Noticed" ("Daj Się Poznać") and will write about my developement at [MD Tech Blog:Mars-Buggy](http://www.md-techblog.net.pl/tags/Mars-Buggy) [PL].

### Building
You need to have mono & fsharp installed (or .NET 4.5.1 with F# targets). First you need to restore NuGet packages (either `nuget restore` or build from Visual Studio). Then simply run `xbuild`/`msbuild` in the project folder.

The compiled application can be found in `bin/Debug` and run with `mono`.
