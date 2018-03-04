#region --- OpenTK.OpenAL License ---

/* AlcFunctions.cs
 * C header: \OpenAL 1.1 SDK\include\Alc.h
 * Spec: http://www.openal.org/openal_webstf/specs/OpenAL11Specification.pdf
 * Copyright (c) 2008 Christoph Brandtner and Stefanos Apostolopoulos
 * See license.txt for license details
 * http://www.OpenTK.net */

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using OpenTK;
using Oxy.Framework.OpenAL.AL;

/* Type Mapping
// 8-bit boolean 
typedef char ALCboolean;
 * byte
// character 
typedef char ALCchar;
 * byte
// signed 8-bit 2's complement integer 
typedef char ALCbyte;
 * byte

// unsigned 8-bit integer 
typedef unsigned char ALCubyte;
 * ubyte

// signed 16-bit 2's complement integer 
typedef short ALCshort;
 * short

// unsigned 16-bit integer 
typedef unsigned short ALCushort;
 * ushort

// unsigned 32-bit integer 
typedef unsigned int ALCuint;
 * uint
  
// signed 32-bit 2's complement integer 
typedef int ALCint;
 * int
// non-negative 32-bit binary integer size
typedef int ALCsizei;
 * int
// enumerated 32-bit value
typedef int ALCenum;
 * int

// 32-bit IEEE754 floating-point
typedef float ALCfloat;
 * float

// 64-bit IEEE754 floating-point
typedef double ALCdouble;
 * double
 
// void type (for opaque pointers only)
typedef void ALCvoid;
 * void
 
 * ALCdevice
 * ALCcontext *context
 * IntPtr
*/

namespace Oxy.Framework.OpenAL.Alc
{
  /// <summary>Alc = Audio Library Context</summary>
  public static class Alc
  {
    #region Constants

    #endregion Constants

    #region Context Management

    #region CreateContext

    private unsafe delegate IntPtr d_alcCreateContext(IntPtr device, int* attrList);

    private static readonly d_alcCreateContext ptr_alcCreateContext =
      AL.AL.ALNativeLib.LoadFunctionPointer<d_alcCreateContext>("alcCreateContext");

    private static unsafe IntPtr sys_CreateContext([In] IntPtr device, [In] int* attrlist)
    {
      return ptr_alcCreateContext(device, attrlist);
    }

    /// <summary>This function creates a context using a specified device.</summary>
    /// <param name="device">a pointer to a device</param>
    /// <param name="attrlist">
    ///   a pointer to a set of attributes: ALC_FREQUENCY, ALC_MONO_SOURCES, ALC_REFRESH,
    ///   ALC_STEREO_SOURCES, ALC_SYNC
    /// </param>
    /// <returns>
    ///   Returns a pointer to the new context (NULL on failure). The attribute list can be NULL, or a zero terminated
    ///   list of integer pairs composed of valid ALC attribute tokens and requested values.
    /// </returns>
    [CLSCompliant(false)]
    public static unsafe ContextHandle CreateContext([In] IntPtr device, [In] int* attrlist)
    {
      return new ContextHandle(sys_CreateContext(device, attrlist));
    }

    // ALC_API ALCcontext *    ALC_APIENTRY alcCreateContext( ALCdevice *device, const ALCint* attrlist );

    /// <summary>This function creates a context using a specified device.</summary>
    /// <param name="device">a pointer to a device</param>
    /// <param name="attriblist">
    ///   an array of a set of attributes: ALC_FREQUENCY, ALC_MONO_SOURCES, ALC_REFRESH,
    ///   ALC_STEREO_SOURCES, ALC_SYNC
    /// </param>
    /// <returns>Returns a pointer to the new context (NULL on failure).</returns>
    /// <remarks>
    ///   The attribute list can be NULL, or a zero terminated list of integer pairs composed of valid ALC attribute
    ///   tokens and requested values.
    /// </remarks>
    public static ContextHandle CreateContext(IntPtr device, int[] attriblist)
    {
      unsafe
      {
        fixed (int* attriblist_ptr = attriblist)
        {
          return CreateContext(device, attriblist_ptr);
        }
      }
    }

    #endregion

    private static bool MakeContextCurrent(IntPtr context)
    {
      return MakeContextCurrent_dptr(context);
    }

    private static readonly MakeContextCurrent_d MakeContextCurrent_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<MakeContextCurrent_d>("alcMakeContextCurrent");

    private delegate bool MakeContextCurrent_d(IntPtr context);

    /// <summary>This function makes a specified context the current context.</summary>
    /// <param name="context">A pointer to the new context.</param>
    /// <returns>Returns True on success, or False on failure.</returns>
    public static bool MakeContextCurrent(ContextHandle context)
    {
      return MakeContextCurrent(context.Handle);
    }
    // ALC_API ALCboolean      ALC_APIENTRY alcMakeContextCurrent( ALCcontext *context );

    private static void ProcessContext(IntPtr context)
    {
      ProcessContext_dptr(context);
    }

    private static readonly ProcessContext_d ProcessContext_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<ProcessContext_d>("alcProcessContext");

    private delegate void ProcessContext_d(IntPtr context);

    /// <summary>
    ///   This function tells a context to begin processing. When a context is suspended, changes in OpenAL state will
    ///   be accepted but will not be processed. alcSuspendContext can be used to suspend a context, and then all the OpenAL
    ///   state changes can be applied at once, followed by a call to alcProcessContext to apply all the state changes
    ///   immediately. In some cases, this procedure may be more efficient than application of properties in a non-suspended
    ///   state. In some implementations, process and suspend calls are each a NOP.
    /// </summary>
    /// <param name="context">a pointer to the new context</param>
    public static void ProcessContext(ContextHandle context)
    {
      ProcessContext(context.Handle);
    }
    // ALC_API void            ALC_APIENTRY alcProcessContext( ALCcontext *context );

    private static void SuspendContext(IntPtr context)
    {
      SuspendContext_dptr(context);
    }

    private static readonly SuspendContext_d SuspendContext_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<SuspendContext_d>("alcSuspendContext");

    private delegate void SuspendContext_d(IntPtr context);

    /// <summary>
    ///   This function suspends processing on a specified context. When a context is suspended, changes in OpenAL state
    ///   will be accepted but will not be processed. A typical use of alcSuspendContext would be to suspend a context, apply
    ///   all the OpenAL state changes at once, and then call alcProcessContext to apply all the state changes at once. In some
    ///   cases, this procedure may be more efficient than application of properties in a non-suspended state. In some
    ///   implementations, process and suspend calls are each a NOP.
    /// </summary>
    /// <param name="context">a pointer to the context to be suspended.</param>
    public static void SuspendContext(ContextHandle context)
    {
      SuspendContext(context.Handle);
    }
    // ALC_API void            ALC_APIENTRY alcSuspendContext( ALCcontext *context );

    private static void DestroyContext(IntPtr context)
    {
      DestroyContext_dptr(context);
    }

    private static readonly DestroyContext_d DestroyContext_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<DestroyContext_d>("alcDestroyContext");

    private delegate void DestroyContext_d(IntPtr context);

    /// <summary>This function destroys a context.</summary>
    /// <param name="context">a pointer to the new context.</param>
    public static void DestroyContext(ContextHandle context)
    {
      DestroyContext(context.Handle);
    }
    // ALC_API void            ALC_APIENTRY alcDestroyContext( ALCcontext *context );

    private static IntPtr sys_GetCurrentContext()
    {
      return sys_GetCurrentContext_dptr();
    }

    private static readonly sys_GetCurrentContext_d sys_GetCurrentContext_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<sys_GetCurrentContext_d>("alcGetCurrentContext");

    private delegate IntPtr sys_GetCurrentContext_d();

    /// <summary>This function retrieves the current context.</summary>
    /// <returns>Returns a pointer to the current context.</returns>
    public static ContextHandle GetCurrentContext()
    {
      return new ContextHandle(sys_GetCurrentContext());
    }
    // ALC_API ALCcontext *    ALC_APIENTRY alcGetCurrentContext( void );

    private static IntPtr GetContextsDevice(IntPtr context)
    {
      return GetContextsDevice_dptr(context);
    }

    private static readonly GetContextsDevice_d GetContextsDevice_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<GetContextsDevice_d>("alcGetContextsDevice");

    private delegate IntPtr GetContextsDevice_d(IntPtr context);

    /// <summary>This function retrieves a context's device pointer.</summary>
    /// <param name="context">a pointer to a context.</param>
    /// <returns>Returns a pointer to the specified context's device.</returns>
    public static IntPtr GetContextsDevice(ContextHandle context)
    {
      return GetContextsDevice(context.Handle);
    }
    // ALC_API ALCdevice*      ALC_APIENTRY alcGetContextsDevice( ALCcontext *context );

    #endregion Context Management

    #region Device Management

    /// <summary>This function opens a device by name.</summary>
    /// <param name="devicename">a null-terminated string describing a device.</param>
    /// <returns>Returns a pointer to the opened device. The return value will be NULL if there is an error.</returns>
    public static IntPtr OpenDevice([In] string devicename)
    {
      return OpenDevice_dptr(devicename);
    }

    private static readonly OpenDevice_d OpenDevice_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<OpenDevice_d>("alcOpenDevice");

    private delegate IntPtr OpenDevice_d([In] string devicename);
    // ALC_API ALCdevice *     ALC_APIENTRY alcOpenDevice( const ALCchar *devicename );

    /// <summary>This function closes a device by name.</summary>
    /// <param name="device">a pointer to an opened device</param>
    /// <returns>
    ///   True will be returned on success or False on failure. Closing a device will fail if the device contains any
    ///   contexts or buffers.
    /// </returns>
    public static bool CloseDevice([In] IntPtr device)
    {
      return CloseDevice_dptr(device);
    }

    private static readonly CloseDevice_d CloseDevice_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<CloseDevice_d>("alcCloseDevice");

    private delegate bool CloseDevice_d([In] IntPtr device);
    // ALC_API ALCboolean      ALC_APIENTRY alcCloseDevice( ALCdevice *device );

    #endregion Device Management

    #region Error support.

    /// <summary>This function retrieves the current context error state.</summary>
    /// <param name="device">a pointer to the device to retrieve the error state from</param>
    /// <returns>Errorcode Int32.</returns>
    public static AlcError GetError([In] IntPtr device)
    {
      return GetError_dptr(device);
    }

    private static readonly GetError_d GetError_dptr = AL.AL.ALNativeLib.LoadFunctionPointer<GetError_d>("alcGetError");

    private delegate AlcError GetError_d([In] IntPtr device);
    // ALC_API ALCenum         ALC_APIENTRY alcGetError( ALCdevice *device );

    #endregion Error support.

    #region Extension support.

    /// <summary>This function queries if a specified context extension is available.</summary>
    /// <param name="device">a pointer to the device to be queried for an extension.</param>
    /// <param name="extname">a null-terminated string describing the extension.</param>
    /// <returns>Returns True if the extension is available, False if the extension is not available.</returns>
    public static bool IsExtensionPresent([In] IntPtr device, [In] string extname)
    {
      return IsExtensionPresent_dptr(device, extname);
    }

    private static readonly IsExtensionPresent_d IsExtensionPresent_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<IsExtensionPresent_d>("alcIsExtensionPresent");

    private delegate bool IsExtensionPresent_d([In] IntPtr device, [In] string extname);
    // ALC_API ALCboolean      ALC_APIENTRY alcIsExtensionPresent( ALCdevice *device, const ALCchar *extname );

    /// <summary>This function retrieves the address of a specified context extension function.</summary>
    /// <param name="device">a pointer to the device to be queried for the function.</param>
    /// <param name="funcname">a null-terminated string describing the function.</param>
    /// <returns>Returns the address of the function, or NULL if it is not found.</returns>
    public static IntPtr GetProcAddress([In] IntPtr device, [In] string funcname)
    {
      return GetProcAddress_dptr(device, funcname);
    }

    private static readonly GetProcAddress_d GetProcAddress_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<GetProcAddress_d>("alcGetProcAddress");

    private delegate IntPtr GetProcAddress_d([In] IntPtr device, [In] string funcname);
    // ALC_API void  *         ALC_APIENTRY alcGetProcAddress( ALCdevice *device, const ALCchar *funcname );

    /// <summary>This function retrieves the enum value for a specified enumeration name.</summary>
    /// <param name="device">a pointer to the device to be queried.</param>
    /// <param name="enumname">a null terminated string describing the enum value.</param>
    /// <returns>
    ///   Returns the enum value described by the enumName string. This is most often used for querying an enum value
    ///   for an ALC extension.
    /// </returns>
    public static int GetEnumValue([In] IntPtr device, [In] string enumname)
    {
      return GetEnumValue_dptr(device, enumname);
    }

    private static readonly GetEnumValue_d GetEnumValue_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<GetEnumValue_d>("alcGetEnumValue");

    private delegate int GetEnumValue_d([In] IntPtr device, [In] string enumname);
    // ALC_API ALCenum         ALC_APIENTRY alcGetEnumValue( ALCdevice *device, const ALCchar *enumname );

    #endregion Extension support.

    #region Query functions

    private delegate IntPtr GetStringPrivate_d([In] IntPtr device, AlcGetString param);

    private static readonly GetStringPrivate_d alcGetString_ptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<GetStringPrivate_d>("alcGetString");

    private static IntPtr GetStringPrivate([In] IntPtr device, AlcGetString param)
    {
      return alcGetString_ptr(device, param);
    }
    // ALC_API const ALCchar * ALC_APIENTRY alcGetString( ALCdevice *device, ALCenum param );

    /// <summary>This function returns pointers to strings related to the context.</summary>
    /// <remarks>
    ///   ALC_DEFAULT_DEVICE_SPECIFIER will return the name of the default output device.
    ///   ALC_CAPTURE_DEFAULT_DEVICE_SPECIFIER will return the name of the default capture device.
    ///   ALC_DEVICE_SPECIFIER will return the name of the specified output device if a pointer is supplied, or will return a
    ///   list of all available devices if a NULL device pointer is supplied. A list is a pointer to a series of strings
    ///   separated by NULL characters, with the list terminated by two NULL characters. See Enumeration Extension for more
    ///   details.
    ///   ALC_CAPTURE_DEVICE_SPECIFIER will return the name of the specified capture device if a pointer is supplied, or will
    ///   return a list of all available devices if a NULL device pointer is supplied.
    ///   ALC_EXTENSIONS returns a list of available context extensions, with each extension separated by a space and the list
    ///   terminated by a NULL character.
    /// </remarks>
    /// <param name="device">a pointer to the device to be queried.</param>
    /// <param name="param">
    ///   an attribute to be retrieved: ALC_DEFAULT_DEVICE_SPECIFIER, ALC_CAPTURE_DEFAULT_DEVICE_SPECIFIER,
    ///   ALC_DEVICE_SPECIFIER, ALC_CAPTURE_DEVICE_SPECIFIER, ALC_EXTENSIONS
    /// </param>
    /// <returns>A string containing the name of the Device.</returns>
    public static string GetString(IntPtr device, AlcGetString param)
    {
      IntPtr pstr = GetStringPrivate(device, param);
      string str = string.Empty;
      if (pstr != IntPtr.Zero) str = Marshal.PtrToStringAnsi(pstr);
      return str;
    }

    /// <summary>This function returns a List of strings related to the context.</summary>
    /// <remarks>
    ///   ALC_DEVICE_SPECIFIER will return the name of the specified output device if a pointer is supplied, or will return a
    ///   list of all available devices if a NULL device pointer is supplied. A list is a pointer to a series of strings
    ///   separated by NULL characters, with the list terminated by two NULL characters. See Enumeration Extension for more
    ///   details.
    ///   ALC_CAPTURE_DEVICE_SPECIFIER will return the name of the specified capture device if a pointer is supplied, or will
    ///   return a list of all available devices if a NULL device pointer is supplied.
    ///   ALC_EXTENSIONS returns a list of available context extensions, with each extension separated by a space and the list
    ///   terminated by a NULL character.
    /// </remarks>
    /// <param name="device">a pointer to the device to be queried.</param>
    /// <param name="param">
    ///   an attribute to be retrieved: ALC_DEVICE_SPECIFIER, ALC_CAPTURE_DEVICE_SPECIFIER,
    ///   ALC_ALL_DEVICES_SPECIFIER
    /// </param>
    /// <returns>A List of strings containing the names of the Devices.</returns>
    public static IList<string> GetString(IntPtr device, AlcGetStringList param)
    {
      List<string> result = new List<string>();

      // We cannot use Marshal.PtrToStringAnsi(),
      //  because alcGetString is defined to return either a nul-terminated string,
      //  or an array of nul-terminated strings terminated by an extra nul.
      // Marshal.PtrToStringAnsi() will fail in the latter case (it will only
      // return the very first string in the array.)
      // We'll have to marshal this ourselves.
      IntPtr t = GetStringPrivate(device, (AlcGetString) param);
      if (t != IntPtr.Zero)
      {
        StringBuilder sb = new StringBuilder();
        byte b;
        int offset = 0;
        do
        {
          b = Marshal.ReadByte(t, offset++);
          if (b != 0)
          {
            sb.Append((char) b);
          }
          else
          {
            // One string from the array is complete
            result.Add(sb.ToString());

            // Check whether the array has finished
            // Note: offset already been increased through offset++ above
            if (Marshal.ReadByte(t, offset) == 0)
              break;
            sb.Remove(0, sb.Length);
          }
        } while (true);
      }
      else
      {
        Debug.Print("[Audio] Alc.GetString({0}, {1}) returned null.",
          device, param);
      }

      return result;
    }

    private static unsafe void GetInteger(IntPtr device, AlcGetInteger param, int size, int* data)
    {
      GetInteger_dptr(device, param, size, data);
    }

    private static readonly GetInteger_d GetInteger_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<GetInteger_d>("alcGetIntegerv");

    private unsafe delegate void GetInteger_d(IntPtr device, AlcGetInteger param, int size, int* data);
    // ALC_API void            ALC_APIENTRY alcGetIntegerv( ALCdevice *device, ALCenum param, ALCsizei size, ALCint *buffer );

    /// <summary>This function returns integers related to the context.</summary>
    /// <param name="device">a pointer to the device to be queried.</param>
    /// <param name="param">
    ///   an attribute to be retrieved: ALC_MAJOR_VERSION, ALC_MINOR_VERSION, ALC_ATTRIBUTES_SIZE,
    ///   ALC_ALL_ATTRIBUTES
    /// </param>
    /// <param name="size">the size of the destination buffer provided, in number of integers.</param>
    /// <param name="data">a pointer to the buffer to be returned</param>
    public static void GetInteger(IntPtr device, AlcGetInteger param, int size, out int data)
    {
      unsafe
      {
        fixed (int* data_ptr = &data)
        {
          GetInteger(device, param, size, data_ptr);
        }
      }
    }

    /// <summary>This function returns integers related to the context.</summary>
    /// <param name="device">a pointer to the device to be queried.</param>
    /// <param name="param">
    ///   an attribute to be retrieved: ALC_MAJOR_VERSION, ALC_MINOR_VERSION, ALC_ATTRIBUTES_SIZE,
    ///   ALC_ALL_ATTRIBUTES
    /// </param>
    /// <param name="size">the size of the destination buffer provided, in number of integers.</param>
    /// <param name="data">a pointer to the buffer to be returned</param>
    public static void GetInteger(IntPtr device, AlcGetInteger param, int size, int[] data)
    {
      unsafe
      {
        fixed (int* data_ptr = data)
        {
          GetInteger(device, param, size, data_ptr);
        }
      }
    }

    #endregion Query functions

    #region Capture functions

    /// <summary>This function opens a capture device by name. </summary>
    /// <param name="devicename">a pointer to a device name string.</param>
    /// <param name="frequency">the frequency that the buffer should be captured at.</param>
    /// <param name="format">the requested capture buffer format.</param>
    /// <param name="buffersize">the size of the capture buffer in samples, not bytes.</param>
    /// <returns>Returns the capture device pointer, or NULL on failure.</returns>
    public static IntPtr CaptureOpenDevice(string devicename, uint frequency, ALFormat format, int buffersize)
    {
      return CaptureOpenDevice_dptr(devicename, frequency, format, buffersize);
    }

    private static readonly CaptureOpenDevice_d CaptureOpenDevice_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<CaptureOpenDevice_d>("alcCaptureOpenDevice");

    private delegate IntPtr CaptureOpenDevice_d(string devicename, uint frequency, ALFormat format, int buffersize);

    public static IntPtr CaptureOpenDevice(string devicename, int frequency, ALFormat format, int buffersize)
    {
      return CaptureOpenDeviceS_dptr(devicename, frequency, format, buffersize);
    }

    private static readonly CaptureOpenDeviceS_d CaptureOpenDeviceS_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<CaptureOpenDeviceS_d>("alcCaptureOpenDevice");

    private delegate IntPtr CaptureOpenDeviceS_d(string devicename, int frequency, ALFormat format, int buffersize);

    /// <summary>This function closes the specified capture device.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <returns>Returns True if the close operation was successful, False on failure.</returns>
    public static bool CaptureCloseDevice([In] IntPtr device)
    {
      return CaptureCloseDevice_dptr(device);
    }

    private static readonly CaptureCloseDevice_d CaptureCloseDevice_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<CaptureCloseDevice_d>("alcCaptureCloseDevice");

    private delegate bool CaptureCloseDevice_d([In] IntPtr device);
    // ALC_API ALCboolean      ALC_APIENTRY alcCaptureCloseDevice( ALCdevice *device );

    /// <summary>This function begins a capture operation.</summary>
    /// <remarks>
    ///   alcCaptureStart will begin recording to an internal ring buffer of the size specified when opening the capture
    ///   device. The application can then retrieve the number of samples currently available using the ALC_CAPTURE_SAPMPLES
    ///   token with alcGetIntegerv. When the application determines that enough samples are available for processing, then it
    ///   can obtain them with a call to alcCaptureSamples.
    /// </remarks>
    /// <param name="device">a pointer to a capture device.</param>
    public static void CaptureStart([In] IntPtr device)
    {
      CaptureStart_dptr(device);
    }

    private static readonly CaptureStart_d CaptureStart_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<CaptureStart_d>("alcCaptureStart");

    private delegate void CaptureStart_d([In] IntPtr device);
    // ALC_API void            ALC_APIENTRY alcCaptureStart( ALCdevice *device );

    /// <summary>This function stops a capture operation.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    public static void CaptureStop([In] IntPtr device)
    {
      CaptureStop_dptr(device);
    }

    private static readonly CaptureStop_d CaptureStop_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<CaptureStop_d>("alcCaptureStop");

    private delegate void CaptureStop_d([In] IntPtr device);
    // ALC_API void            ALC_APIENTRY alcCaptureStop( ALCdevice *device );

    /// <summary>This function completes a capture operation, and does not block.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <param name="buffer">a pointer to a buffer, which must be large enough to accommodate the number of samples.</param>
    /// <param name="samples">the number of samples to be retrieved.</param>
    public static void CaptureSamples(IntPtr device, IntPtr buffer, int samples)
    {
      CaptureSamples_dptr(device, buffer, samples);
    }

    private static readonly CaptureSamples_d CaptureSamples_dptr =
      AL.AL.ALNativeLib.LoadFunctionPointer<CaptureSamples_d>("alcCaptureSamples");

    private delegate void CaptureSamples_d(IntPtr device, IntPtr buffer, int samples);
    // ALC_API void            ALC_APIENTRY alcCaptureSamples( ALCdevice *device, ALCvoid *buffer, ALCsizei samples );

    /// <summary>This function completes a capture operation, and does not block.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <param name="buffer">a reference to a buffer, which must be large enough to accommodate the number of samples.</param>
    /// <param name="samples">the number of samples to be retrieved.</param>
    public static void CaptureSamples<T>(IntPtr device, ref T buffer, int samples)
      where T : struct
    {
      GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
      try
      {
        CaptureSamples(device, handle.AddrOfPinnedObject(), samples);
      }
      finally
      {
        handle.Free();
      }
    }

    /// <summary>This function completes a capture operation, and does not block.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <param name="buffer">a buffer, which must be large enough to accommodate the number of samples.</param>
    /// <param name="samples">the number of samples to be retrieved.</param>
    [CLSCompliant(false)]
    public static void CaptureSamples<T>(IntPtr device, T[] buffer, int samples)
      where T : struct
    {
      CaptureSamples(device, ref buffer[0], samples);
    }

    /// <summary>This function completes a capture operation, and does not block.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <param name="buffer">a buffer, which must be large enough to accommodate the number of samples.</param>
    /// <param name="samples">the number of samples to be retrieved.</param>
    [CLSCompliant(false)]
    public static void CaptureSamples<T>(IntPtr device, T[,] buffer, int samples)
      where T : struct
    {
      CaptureSamples(device, ref buffer[0, 0], samples);
    }

    /// <summary>This function completes a capture operation, and does not block.</summary>
    /// <param name="device">a pointer to a capture device.</param>
    /// <param name="buffer">a buffer, which must be large enough to accommodate the number of samples.</param>
    /// <param name="samples">the number of samples to be retrieved.</param>
    [CLSCompliant(false)]
    public static void CaptureSamples<T>(IntPtr device, T[,,] buffer, int samples)
      where T : struct
    {
      CaptureSamples(device, ref buffer[0, 0, 0], samples);
    }

    #endregion Capture functions
  }
}