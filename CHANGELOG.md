## 0.2 Releases:

### 0.2.0 Progressive Marmot

**22.04.2018 - `(unstable)`**

- Moved to MonoGame as base framework, instead of OpenTK
- Removed OxyPlayground
- OxyFramework now becomes part of OxyEngine
- Python scripting support moves to OxyEngine.Python package
- Entity Component System base, also:
  - Ready to use components
  - Ready to use entities
  - Implemented systems: DrawSystem, GenericSystem
- Add Python stacktrace on exception
- Resources module caching
- Input module update: 
  - Support for gamepads
  - Get/set mouse position
  - Get mouse wheel state
- Graphics module update:
  - Add ability to draw only part of Texture using Rectangle (#32)
  - Add ability to set origin point for Graphics.Draw (#29)
- Audio module update:
  - Support of the formats .mp3, .ogg
- Alt+F4 to close window on Windows (#28)
- Various bug fixes

## 0.1 Releases:

### 0.1.1 Early Aurora

**10.03.2018 - `(unstable)`**

- Fix NuGet package (#21, #23)
- Fix pystdlib error at Playground's startup (#22)
- Fix Python import from project folder and subfolders (#20)

### 0.1.0 Early Aurora

**08.03.2018 - `(unstable)`**

- Initial release
- Python 2.x support using IronPython
- Resources import (fonts, textures, sounds) from file or from System.Stream
- Text printing (using TTF or bitmap fonts)
- Texture drawing
- Primities drawing (points, lines, circles, rectangles, polygons) with variable line width and stroke/fill mode
- Matrix transformations
- Audio playing support (wav)
- Keyboard and mouse input support
- Window management, error screen, debug mode
- Support for Windows, Max OS X, Linux using Mono