using System;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using SharpFont;

namespace QuickFont
{
	/// <summary>
	/// An implementation of <see cref="IFont"/> that uses FreeType via
	/// SharpFont to load the font file. This implementation supports reading
	/// kerning information directly from the font file.
	/// </summary>
	public class FreeTypeFont : IFont
	{
	  private PrivateFontCollection _collection;
	  private Font _font;
	  
		private const uint DPI = 96;

		/// <summary>
		/// The size of the font
		/// </summary>
		public float Size { get; private set; }

	  /// <summary>
	  /// Whether the font has kerning information available, or if it needs
	  /// to be calculated
	  /// </summary>
	  public bool HasKerningInformation => false;

		/// <summary>
		/// Creates a new instace of FreeTypeFont
		/// </summary>
		/// <param name="fontPath">The path to the font file</param>
		/// <param name="size">Size of the font</param>
		/// <param name="style">Style of the font</param>
		/// <param name="superSampleLevels">Super sample levels</param>
		/// <param name="scale">Scale</param>
		/// <exception cref="ArgumentException"></exception>
		public FreeTypeFont(string fontPath, float size, FontStyle style, int superSampleLevels = 1, float scale = 1.0f)
		{
		  _collection = new PrivateFontCollection();
		  
			// Check that the font exists
			if (!File.Exists(fontPath)) 
			  throw new ArgumentException("The specified font path does not exist", nameof(fontPath));
		  
			LoadFontFace(fontPath, size, style, superSampleLevels, scale);
		}

		private void LoadFontFace(string fontPath, float size, FontStyle fontStyle, int superSampleLevels, float scale)
		{
		  _collection.AddFontFile(fontPath);

		  if (!_collection.Families[0].IsStyleAvailable(fontStyle))
		    throw new NotSupportedException("Could not find correct face style in font: " + fontStyle);
		  
		  Size = size * scale * superSampleLevels;
		  
		  _font = new Font(_collection.Families[0].Name, Size, fontStyle);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return _font.FontFamily.Name ?? "";
		}

		/// <summary>
		/// Draws the given string at the specified location
		/// </summary>
		/// <param name="s">The string to draw</param>
		/// <param name="graph">The graphics surface to draw the string on to</param>
		/// <param name="color">The color of the text</param>
		/// <param name="x">The x position of the string</param>
		/// <param name="y">The y position of the string</param>
		/// <returns>Returns the offset of the glyph from the given x and y. Only non-zero with <see cref="FreeTypeFont"/></returns>
		public Point DrawString(string s, Graphics graph, Brush color, int x, int y)
		{
			// Check we are only passed a single character
			if (s.Length > 1)
				throw new ArgumentOutOfRangeException(nameof(s), "Implementation currently only supports drawing individual characters");

			// Check the brush is a solid colour brush
			if (!(color is SolidBrush))
				throw new ArgumentException("Brush is required to be a SolidBrush (single, solid color)", nameof(color));

			var fontColor = ((SolidBrush) color).Color;

		  graph.DrawString(s, _font, color, new Point(x, y));
		  
		  return Point.Empty;
		  
//			// Load the glyph into the face's glyph slot
//			LoadGlyph(s[0]);
//
//			// Render the glyph
//			_fontFace.Glyph.RenderGlyph(RenderMode.Normal);
//
//			// If glyph rendered correctly, copy onto graphics
//			if (_fontFace.Glyph.Bitmap.Width > 0)
//			{
//				var bitmap = _fontFace.Glyph.Bitmap.ToGdipBitmap(fontColor);
//				int baseline = y + _maxHorizontalBearyingY;
//				graph.DrawImageUnscaled(bitmap, x, (baseline - _fontFace.Glyph.Metrics.HorizontalBearingY.Ceiling()));
//				return new Point(0, baseline - _fontFace.Glyph.Metrics.HorizontalBearingY.Ceiling() - 2*y);
//			}
//
//			return Point.Empty;
		}

		/// <summary>
		/// Gets the kerning between the given characters, if the font supports it
		/// </summary>
		/// <param name="c1">The first character of the character pair</param>
		/// <param name="c2">The second character of the character pair</param>
		/// <returns>The horizontal kerning offset of the character pair</returns>
		public int GetKerning(char c1, char c2) =>
		  throw new NotSupportedException();

		/// <summary>
		/// Measures the given string and returns the size
		/// </summary>
		/// <param name="s">The string to measure</param>
		/// <param name="graph">The graphics surface to use for temporary purposes</param>
		/// <returns>The size of the given string</returns>
		public SizeF MeasureString(string s, Graphics graph)
		{
			// Check we are only passed a single character
			if (s.Length > 1)
				throw new ArgumentOutOfRangeException(nameof(s), "Implementation currently only supports drawing individual characters");

		  return graph.MeasureString(s, _font);
		}

		private bool _disposedValue; // To detect redundant calls

		/// <summary>
		/// Dispose resources 
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					if (_font != null)
					{
					  _font.Dispose();
					  _font = null;
					}
          if (_collection != null)
          {
            _collection.Dispose();
            _collection = null;
          }
				}
				_disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
		}
	}
}
