![OxyEngine](https://i.imgur.com/BjjCdjB.png)

**OxyEngine** is a full-featured cross-platform 2D game engine that allows you to develop games using **C#** and **Python**.

| :speech_balloon: | :white_check_mark: | `develop` | `master` |
|---------|---------|---------|--------|
| [![Gitter chat](https://badges.gitter.im/gitterHQ/gitter.png)](https://gitter.im/OxyEngine/Lobby) | [![BCH compliance](https://bettercodehub.com/edge/badge/OxyTeam/OxyEngine?branch=develop)](https://bettercodehub.com/) | [![Build status](https://ci.appveyor.com/api/projects/status/tyg13hjkm01vb3yd/branch/develop?svg=true)](https://ci.appveyor.com/project/AndrewRublyov/oxyengine-y2q1n/branch/develop) | [![Build status](https://ci.appveyor.com/api/projects/status/tyg13hjkm01vb3yd/branch/master?svg=true)](https://ci.appveyor.com/project/AndrewRublyov/oxyengine-y2q1n/branch/master) |

---

:exclamation::exclamation::exclamation:
***Please note: 
This project in alpha stage of development at that moment. Do not use it for any production projects before first public release will be published. For now, OxyEngine does not guarantee backward compatibility of API and stability of your code.***

---


# Getting Started

These instructions will get you information to get started with engine.


## Prerequisites

### Mono

If you are on Linux or MacOS, you need to install Mono SDK for debugging and running .NET executables.

Latest Mono SDK can be found **[here](https://www.mono-project.com/download/stable/)**.

### MonoGame

You need to install MonoGame SDK to compile assets and using ready-to-use remplates.

Latest MonoGame SDK can be found **[here](http://www.monogame.net/downloads/)**.


## Tutorials

You can found tutorials on the **[wiki page](wiki)**.

## Compatibility

### Desktop
OxyEngine has only 2 native dependencies for Desktop from MonoGame: **OpenAL** and **SDL2**. Both of them are shipped with OxyPlayground and you also need to provide these libs to get OxyEngine project to launch.

More info in first tutorial.

## Installing
This section will show you how to install OxyEngine into your .NET project.

### From NuGet (preffered)
We are using custom MyGet feed. You need to add this feed to your NuGet PM.

#### NuGet V3:
`https://www.myget.org/F/oxyteam/api/v3/index.json`

#### NuGet V2:
`https://www.myget.org/F/oxyteam/api/v2`

After that, install Oxy.Engine **[NuGet package](https://www.myget.org/feed/oxyteam/package/nuget/Oxy.Framework)**. This is best choice for beginners.

### Building from sources (advanced)
To install stable version of OxyEngine into your .NET Core App:
1. Сlone this repository:
 `git clone https://github.com/OxyTeam/OxyEngine.git` 
  Use **master** branch for only stable and production-ready code. 
  You also can use **default** branch, but it may be unstable.
2. Select preffered configuration and Build solution.
3. Reference OxyEngine.dll and other dependencies to you project.
  
  
# Examples

**[All examples](#)**


# Changelog

See **[CHANGELOG.md](CHANGELOG.md)** for changes.

# Built With
OxyEngine uses some third-party libraries and tools:

* **[MonoGame](http://www.monogame.net/)** - One framework for creating powerful cross-platform games.
* **[IronPython](http://ironpython.net/)** - the Python programming language implementations for the .NET platform.


# Contributing

Please read **[CONTRIBUTING.md](CONTRIBUTING.md)** for details on our code of conduct, and the process for submitting pull requests to us.


# Versioning

We use **[Semantic Versioning](http://semver.org/)** for versioning. For the versions available, see the **[tags on this repository](https://github.com/OxyTeam/OxyEngine/tags)**. 


# Authors

* **Andrey Rublyov** - *Programmer* - **[AndrewRublyov](https://github.com/AndrewRublyov)**
* **Andrey Belyaev** - *Programmer* - **[Svetomech](https://github.com/Svetomech)**

See also the list of **[contributors](https://github.com/OxyTeam/OxyEngine/contributors)** who participated in this project.


# License

This project is licensed under the **MIT License** - see the **[LICENSE](LICENSE)** file for details


# Acknowledgments

Big thanks to:
* **Love2d** team for inspiration for OxyEngine API.
* **Guys from MonoGame team** for great cross-platform game framework.
* **IronLaungages developers** for supporting and development of IronPython and other Iron languages.

