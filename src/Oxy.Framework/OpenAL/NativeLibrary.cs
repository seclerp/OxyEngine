using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Oxy.Framework.OpenAL
{
  internal abstract class NativeLibrary : IDisposable
  {
    private readonly string _libraryName;

    public NativeLibrary(string libraryName)
    {
      _libraryName = libraryName;
      NativeHandle = LoadLibrary(_libraryName);
      if (NativeHandle == IntPtr.Zero) throw new InvalidOperationException("Could not load " + libraryName);
    }

    public void Dispose()
    {
      FreeLibrary(NativeHandle);
    }

    public IntPtr NativeHandle { get; }

    protected abstract IntPtr LoadLibrary(string libraryName);
    protected abstract void FreeLibrary(IntPtr libraryHandle);
    protected abstract IntPtr LoadFunction(string functionName);

    public IntPtr LoadFunctionPointer(string functionName)
    {
      if (functionName == null) throw new ArgumentNullException(nameof(functionName));

      return LoadFunction(functionName);
    }

    public T LoadFunctionPointer<T>(string functionName)
    {
      if (functionName == null) throw new ArgumentNullException(nameof(functionName));

      IntPtr ptr = LoadFunction(functionName);
      if (ptr == IntPtr.Zero)
        return default;
      return Marshal.GetDelegateForFunctionPointer<T>(ptr);
    }


    public static NativeLibrary Load(string libraryName)
    {
#if NETCORE
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        return new WindowsNativeLibrary(libraryName);
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        return new Unix(libraryName);
      throw new PlatformNotSupportedException("Cannot load native libraries on this platform: " +
                                              RuntimeInformation.OSDescription);
#else
      return new WindowsNativeLibrary(libraryName);
#endif
    }

    private class WindowsNativeLibrary : NativeLibrary
    {
      public WindowsNativeLibrary(string libraryName) : base(libraryName)
      {
      }

      protected override IntPtr LoadLibrary(string libraryName)
      {
        return Kernel32.LoadLibrary(libraryName);
      }

      protected override void FreeLibrary(IntPtr libraryHandle)
      {
        Kernel32.FreeLibrary(libraryHandle);
      }

      protected override IntPtr LoadFunction(string functionName)
      {
        Debug.WriteLine("Loading " + functionName);
        return Kernel32.GetProcAddress(NativeHandle, functionName);
      }
    }

    private class Unix : NativeLibrary
    {
      public Unix(string libraryName) : base(libraryName)
      {
      }

      protected override IntPtr LoadLibrary(string libraryName)
      {
        Libdl.dlerror();
        IntPtr handle = Libdl.dlopen(libraryName, Libdl.RTLD_NOW);
        if (handle == IntPtr.Zero && !Path.IsPathRooted(libraryName))
        {
          string localPath = Path.Combine(AppContext.BaseDirectory, libraryName);
          handle = Libdl.dlopen(localPath, Libdl.RTLD_NOW);
        }

        return handle;
      }

      protected override void FreeLibrary(IntPtr libraryHandle)
      {
        Libdl.dlclose(libraryHandle);
      }

      protected override IntPtr LoadFunction(string functionName)
      {
        return Libdl.dlsym(NativeHandle, functionName);
      }
    }
  }

  internal static class Kernel32
  {
    [DllImport("kernel32")]
    public static extern IntPtr LoadLibrary(string fileName);

    [DllImport("kernel32")]
    public static extern IntPtr GetProcAddress(IntPtr module, string procName);

    [DllImport("kernel32")]
    public static extern int FreeLibrary(IntPtr module);
  }

  internal static class Libdl
  {
    public const int RTLD_NOW = 0x002;

    [DllImport("libdl")]
    public static extern IntPtr dlopen(string fileName, int flags);

    [DllImport("libdl")]
    public static extern IntPtr dlsym(IntPtr handle, string name);

    [DllImport("libdl")]
    public static extern int dlclose(IntPtr handle);

    [DllImport("libdl")]
    public static extern string dlerror();
  }
}