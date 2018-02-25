### Modules compatibility table (for latest state of `develop` branch)

|   Module  |       Windows      |       Mac OS X     |        Linux*      |
|-----------|:------------------:|:------------------:|:------------------:|
| Window    | :white_check_mark: | :white_check_mark: |      :warning:     |
| Graphics  | :white_check_mark: | :white_check_mark: |      :warning:     |
| Resources | :white_check_mark: | :white_check_mark: | :white_check_mark: |
| Input     |   :white_circle:   |   :white_circle:   | :white_check_mark: |
| Audio     |      :warning:     | :white_check_mark: |      :warning:     |


 **`*`** only for **[supported Linux distributions](https://github.com/dotnet/core/blob/master/release-notes/2.0/2.0-supported-os.md)**.
 
* :white_check_mark: - works
* :white_circle: - not tested yet
* :warning: - works but may require install or configure something
* :x: - not working

--------

### Windows

#### Audio

Audio module requires `openal32.dll` to be installed in your system. 

If not, please **[download installer](https://www.openal.org/downloads/)**.

Future releases of Oxy.Framework will contain own `openal32.dll` with framework's executable.

#### Other

Other modules may work correctly out-of-the-box.

--------

### Mac OS X

All modules may work correctly out-of-the-box.

--------

### Linux

#### Window & Graphics

Window and Graphics requires some `OpenGL` libraries installed in your system.

For most distributions there are built-in `mesa` (software OpenGL implementation) and all works fine.

But sometimes some headers are not installed and Oxy.Frameworks crushes.

**Known issues:**

* `System.DllNotFoundException: Unable to load DLL 'gbm'`: on some systems fix is to install `mesa-common-dev` package or alternative.

**Global fix**: Try to totally reinstall OpenGL component or to add some dev packages for OpenGL implementation, according to your setup.

#### Audio

Audio requires `OpenAL` libraries installed in your system.

**Known issues:**

* `System.InvalidOperationException: Could not load libopenal.so.1`: install `libopenal1` package

**Global fix**: You should install package `libopenal1` (or alternative package for your system) and most of the problems are go away.

#### Other

Other modules may work correctly out-of-the-box.
