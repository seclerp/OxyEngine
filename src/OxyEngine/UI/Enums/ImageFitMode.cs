namespace OxyEngine.UI.Enums
{
  public enum ImageFitMode
  {
    None,     // Default, no fit
    Stretch,  // Sizes are stretched to fit width and height of image
    Contain,  // The replaced content is scaled to maintain its aspect ratio while fitting within the element's content box
    Cover,    // The replaced content is sized to maintain its aspect ratio while filling the element's entire content box. The object will be clipped to fit
  }
}