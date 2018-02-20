#region --- License ---
/* Copyright (c) 2006, 2007 Stefanos Apostolopoulos
 * See license.txt for license info
 */
#endregion

using System;

namespace Oxy.Framework.OpenTK.Compatibility
{
    /// <summary>
    /// Represents an item that can be packed with the TexturePacker.
    /// </summary>
    /// <typeparam name="T">The type of the packable item.</typeparam>
    interface IPackable<T> : IEquatable<T>
    {
        int Width { get; }
        int Height { get; }
    }
}
