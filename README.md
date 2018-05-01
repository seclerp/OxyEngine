<p align="center">
  <img src="https://i.imgur.com/BjjCdjB.png" alt="OxyEngine">
</p>

<p align="center">
  <b>OxyEngine</b> is a full-featured cross-platform 2D game engine that allows you to develop games using <b>C#</b> and <b>Python</b>.
</p>

<p align="center">
  :speech_balloon: <b><a href="https://gitter.im/OxyEngine/Lobby">Gitter</a></b><br>
  <a style="vertical-align: middle;" href="https://ci.appveyor.com/project/AndrewRublyov/oxyengine-y2q1n/branch/develop"><img src="https://ci.appveyor.com/api/projects/status/tyg13hjkm01vb3yd/branch/develop?svg=true&passingText=develop%20-%20OK&failingText=develop%20-%20Fails"></a><br>
  <!-- https://ci.appveyor.com/api/projects/status/tyg13hjkm01vb3yd/branch/develop?svg=true&passingText=develop%20-%20OK&failingText=develop%20-%20Fails -->
  <a href="https://ci.appveyor.com/project/AndrewRublyov/oxyengine-y2q1n/branch/master"><img src="https://ci.appveyor.com/api/projects/status/tyg13hjkm01vb3yd/branch/master?svg=true"></a>
</p>

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


## Compatibility

### Desktop
OxyEngine has only 2 native dependencies for Desktop from MonoGame: **OpenAL** and **SDL2**. Both of them are shipped with MonoGame.

More info in first tutorial.


## Installing
This section will show you how to install OxyEngine into your .NET project.

### From NuGet (preffered)

#### Latest stable release

Latest release you can find on **[NuGet.org](https://www.nuget.org/packages?q=OxyEngine)**.

#### Developer build

Every successful builded commit to `develop` branch push new nuget package to our custom NuGet feed:

**MyGet feed (v3):** `https://www.myget.org/F/oxyteam/api/v3/index.json`

**MyGet feed (v2):** `https://www.myget.org/F/oxyteam/api/v2`

After adding feeds, install Oxy.Engine **[NuGet package](https://www.myget.org/feed/oxyteam/package/nuget/OxyEngine.Desktop)**.

### Building from sources (advanced)
To install stable version of OxyEngine into your .NET Core App:
1. Сlone this repository:
 `git clone https://github.com/OxyTeam/OxyEngine.git` 
  Use **master** branch for only stable and production-ready code. 
  You also can use **default** branch, but it may be unstable.
2. Select preffered configuration and Build solution.
3. Reference OxyEngine.dll and other dependencies to you project.
  

# Documentation

Actual source reference can be found **[here](https://oxyteam.github.io/docs/)** 

# Tutorials

You can found tutorials on the **[wiki page](wiki)**.
  

# Examples

**[Quick start](https://github.com/OxyTeam/WIki/tree/master/Tutorials/quick-start-for-building-prototypes/QuickStart)**


# Changelog

See **[CHANGELOG.md](CHANGELOG.md)** for changes.


# Built With

OxyEngine uses some third-party libraries and tools:

* **[MonoGame](http://www.monogame.net/)** - One framework for creating powerful cross-platform games.
* **[IronPython](http://ironpython.net/)** - Еhe Python programming language implementations for the .NET platform.
* **[NUnit](http://nunit.org/)** - Еhe most popular unit test framework for .NET.
* **[FakeItEasy](https://fakeiteasy.github.io/)** - The easy mocking library for .NET.


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