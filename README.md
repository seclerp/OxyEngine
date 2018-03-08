![OxyEngine](https://i.imgur.com/BjjCdjB.png)


**OxyEngine** is a full-featured cross-platform 2D game engine that allows you to develop games using **C#** and **Python**.

| `develop` | `master` |
|---------|--------|
| [![Build status](https://ci.appveyor.com/api/projects/status/tyg13hjkm01vb3yd/branch/develop?svg=true)](https://ci.appveyor.com/project/AndrewRublyov/oxyengine-y2q1n/branch/develop) | [![Build status](https://ci.appveyor.com/api/projects/status/tyg13hjkm01vb3yd/branch/master?svg=true)](https://ci.appveyor.com/project/AndrewRublyov/oxyengine-y2q1n/branch/master) |

---

:exclamation::exclamation::exclamation:
***Please note: 
This project in very early stage of development at that moment. Do not use it for any production projects before first public release will be published. For now, OxyEngine does not guarantee backward compatibility of API and stability of your code.***

---
# Getting Started

These instructions will get you information for getting started with engine.

For now, only  low-level API for graphics, input, window and resources modules are implemented. This part of engine is called **Oxy.Framework**.

In future you will be able to create games using high-level **Oxy.Engine** part of the project, such as powerfull visual editor called **Oxy.Editor**.

## Prerequisites

OxyEngine is a **.NET Framework** project. 

On Windows you can build and run OxyEngine using .NET Framework as usual. On Linux and Mac OS X you need to install Mono and run Mono MSBuild to build, or Mono runtime to execute binaries.

**There are also prebuilt binaries for most systems.**

For now, only Desktop operation systems are supported (**Windows, MacOS X, Linux**).

Your graphics card must support **OpenGL 3.2+**.

## Compatibility

Because of platform-specific reasons, some modules may have trouble working on your operation system setup. Please read **[compatibility notes](COMPATIBILITY.md)** for details.

There are also **list of libraries, that must be installed on your machine**. If you have troubles in running applications that use OxyEngine, please check **[this list](CROSSLIBS.md)**.

## Installing
This section will show you how to install OxyEngine into your .NET project.

### From NuGet (preffered)
Just install Oxy.Framework **[NuGet package](https://nuget.org)**. This is best choice for beginners.

### From project sources (advanced)
To install stable version of OxyEngine into your .NET Core App:
* Сlone this repository:
 `git clone https://github.com/OxyTeam/OxyEngine.git` 
  Use **master** branch for only stable and production-ready code. 
  You also can use **default** branch, but it may not build or will work not properly.
* For examples you can build also **Oxy.Playground**
  and reference compiled dll's (Oxy.Framework assembly) to your project.
  **OR** 
  you can reference Oxy.Framework projects directly without using compiled dll's
  
## Hello, World! 

"Hello World!" example using low-level Oxy.Framework.

Also this example use custom .ttf font **that must be placed into your project folder**, you can use any font you want with this example.
  
### Example using only C#

**Program.cs:**
```csharp
using System;
using System.IO;

namespace Oxy.Framework.TestPlayer
{
  class Program
  {
    static void Main(string[] args)
    {
      // Set scripts root folder. All script paths will be relative to this folder
      Common.SetScriptsRoot(Environment.CurrentDirectory);
      // Set library root folder. All asset paths will be relative to this folder
      Common.SetLibraryRoot(Environment.CurrentDirectory);

      // Text object for "Hello, World!" text
      TextObject textObj = null;
      
      // Callback for OnLoad event to load and initialize all things
      Window.OnLoad(() =>
      {
        // Load our custom font
        var font = Resources.LoadFont("roboto.ttf");
        // Create text object
        textObj = Graphics.NewText(font, "Hello, World!");
      });      
      
      // OnDraw is called every frame to draw graphics on screen
      // Graphics.Draw will draw text object on screen
      Window.OnDraw(() => Graphics.Draw(textObj, 50, 50));
      
      // Start gameloop (at 60 FPS by default)
      Window.Show();
    }
  }
}
```
Thats all. Build and run this example to see result.

### Example using Python + C#

Create folder for scripts and resources. Make sure they will be copied to build output folder. You can rename folders to any name you want, but better use this for future compatibility

This is more preffered way to use Oxy.Framework.

**scripts/game.py:**
```python
# Import all Oxy.Framework modules
from Oxy.Framework import *

# This function will be called after window is get ready
def onLoad():
    global textObj
    # Load our custom font
    font = Resources.LoadFont("roboto.ttf", 48)
    # Create text object
    textObj = Graphics.NewText(font, "Hello World OxyEngine!")
    
# This function Will be called every frame to draw graphics on screen
def onDraw():
    # Graphics.Draw will draw text object on screen
    Graphics.Draw(textObj, 50, 50)

# Setup event listeners for Load and Draw
Window.OnLoad(onLoad)
Window.OnDraw(onDraw)

# Start gameloop (at 60 FPS by default)
Window.Show()
```

**Program.cs:**

```csharp
using System;
using System.IO;

namespace Oxy.Framework.TestPlayer
{
  class Program
  {
    static void Main(string[] args)
    {
      // Set scripts root folder. All script paths will be relative to this folder
      Common.SetScriptsRoot(Path.Combine(Environment.CurrentDirectory, "scripts"));
      // Set library root folder. All asset paths will be relative to this folder
      Common.SetLibraryRoot(Path.Combine(Environment.CurrentDirectory, "library"));
      // Execute script
      Common.ExecuteScript("game.py");
    }
  }
}
```

Result of both implementations:

![Hello? World!](https://i.imgur.com/7o3VPSQ.png)


# Built With
OxyEngine uses some third-party libraries and tools:

* [OpenTK](https://github.com/opentk/opentk) - Open Toolkit library is a fast, low-level C# wrapper for OpenGL and OpenAL.
* [IronPython](http://ironpython.net/) - the Python programming language for the .NET Framework
* [DLR](https://github.com/IronLanguages/dlr) - Open source implementation of Dynamic Language Runtime

# Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

# Versioning

We use [Semantic Versioning](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/OxyTeam/OxyEngine/tags). 

# Authors

* **Andrey Rublyov** - *Programmer* - [AndrewRublyov](https://github.com/AndrewRublyov)
* **Andrey Belyaev** - *Programmer* - [Svetomech](https://github.com/Svetomech)

See also the list of [contributors](https://github.com/OxyTeam/OxyEngine/contributors) who participated in this project.

# License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details

# Acknowledgments

Big thanks to:
* **Love2d** team for inspiration for Oxy.Framework API 
* **Guys from OpenTK team** for great low-level OpenGL + OpenAL framework.
* **IronLaungages developers** for supporting and development of IronPython and other Iron languages

