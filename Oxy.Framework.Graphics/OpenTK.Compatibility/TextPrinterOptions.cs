using System;

namespace Oxy.Framework.OpenTK.Compatibility
{
    /// <summary>
    /// Defines available options for the TextPrinter.
    /// </summary>
    [Flags]
    public enum TextPrinterOptions
    {
        /// <summary>The TextPrinter will use default printing options.</summary>
        Default = 0x0000,
        /// <summary>The TextPrinter will not cache text blocks as they are measured or printed.</summary>
        NoCache = 0x0001,
    }
}
