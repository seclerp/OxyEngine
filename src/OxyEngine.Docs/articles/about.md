**OxyEngine** is a cross-platform open-source game engine, focused on 2D development.

## Features

The engine is aimed at a wide variety of gaming platforms:
- Windows, MacOS X, Linux
- Android `(in development)`
- iOS `(in development)`
- PlayStation 4 `(in future)`
- XBox One `(in future)`

It includes such important elements as:
- Drawing textures, sprites, graphic primitives, text
- Playing sounds, music, support for popular audio formats
- Support for a variety of user input methods: keyboard, mouse, gamepad `(mobile and touch input in development)`
- Physical subsystem and resolution of collisions `(in development)`
- Ready-to-use network module `(in development)`

## Philosophy

When developing OxyEngine and all its components, we adhere to two basic principles: **Scalability** and **Flexibility**.

### Flexibility

Many modern game engines are tightly tied to a specific architecture, for example, Entity-Component-System. This allows to solve some important architectural problems, but also creates new obstacles, which often lead to breaking other principles and resorting to crutches. In OxyEngine, you can use exactly what you need. For example, if you have enough OxyPlayground and Python capabilities, you do not need to use OxyEngine in full size and learn C#.

### Scalability

Scalability means the ability to make a simple mini-game more complex without experiencing technical difficulties. In addition, you can easily extend the functionality by connecting additional services `(in development)`.

## What's inside OxyEngine

OxyEngine consists of several components that you can use to develop:
- **OxyEngine**: the engine itself, designed as a .NET library. It contains absolutely all basic modules for development, such as working with graphics, sound, physics and other basic modules. Python scripting, as well as other services are rendered separately.
- **OxyEngine.Python**: Python frontend for scripting. Allows you to run scripts in Python.
- **OxyEditor `(in development)`**: Unity-like visual editor that allows very convenient optimization of development processes, automating and simplifying such routine tasks as project assembly, content compilation, visual editing for scenes, objects, shaders, localizations, and complete publish capabilities.

## MonoGame

OxyEngine is based on MonoGame, which means that you can also use the full power of MonoGame when writing code by connecting the appropriate namespace. If you do not have enough functionality in OxyEngine, but it is present in MonoGame, please let us know :)

More details you can find on **[MonoGame official website](http://monogame.net)**.

## Do you have any more questions?

Then **[contact us](mailto:andrewrublyov99@gmail.com)**, we will definitely answer them!