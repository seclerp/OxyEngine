using System;
using System.IO;
using Oxy.Framework.Objects;

namespace Oxy.Framework.TestPlayer
{
  class Program
  {
    static void Main(string[] args)
    {
      // Set scripts root folder. All script paths will be relative to this folder
      Common.SetScriptsRoot(Path.Combine(Environment.CurrentDirectory, "scripts"));
      // Set library root folder. All asset paths will be relative to this folder
      Common.SetLibraryRoot(Path.Combine(Environment.CurrentDirectory, "library"));

      //Common.ExecuteScript("errors-example.py");
      //ErrorsStub();
      TextureDrawing();
      //PrimitivesExample();
      //PrintingExample();
    }

    static void ErrorsStub()
    {
      Window.OnLoad(() => throw new Exception("Example exception"));
      
      Window.Show();
    }
    
    static void TextureDrawing()
    {
      TextureObject texture = null;
      
      Window.OnLoad(() => texture = Resources.LoadTexture("test.jpg"));
      Window.OnDraw(() => Graphics.Draw(texture, 50, 50));
      
      Window.Show();
    }
    
    static void PrimitivesExample()
    {
      Window.OnLoad(() =>
      {
        Window.SetWidth(375);
        Window.SetHeight(475);
        Window.SetFullscreen(true);
        
        Graphics.SetBackgroundColor(100, 100, 100);
        Graphics.SetColor(0, 255, 0);
      });
      
      Window.OnDraw(() =>
      {
        Graphics.SetColor(0, 255, 0);
        Graphics.SetLineThickness(5);
        Graphics.DrawRectangle("line", 50, 25, 100, 75);
        Graphics.DrawRectangle("fill", 175, 25, 100, 75);
        
        Graphics.SetColor(255, 255, 0);
        Graphics.SetLineThickness(4);
        Graphics.DrawLine(50, 125, 50, 200);
        Graphics.SetLineThickness(3);
        Graphics.DrawLine(75, 125, 75, 200);
        Graphics.SetLineThickness(2);
        Graphics.DrawLine(100, 125, 100, 200);
        Graphics.SetLineThickness(1);
        Graphics.DrawLine(125, 125, 125, 200);
        
        Graphics.SetColor(255, 255, 0);
        Graphics.SetLineThickness(4);
        Graphics.DrawPoint(50, 225);
        Graphics.SetLineThickness(3);
        Graphics.DrawPoint(75, 225);
        Graphics.SetLineThickness(2);
        Graphics.DrawPoint(100, 225);
        Graphics.SetLineThickness(1);
        Graphics.DrawPoint(125, 225);
        
        Graphics.SetColor(50, 255, 255);
        Graphics.SetLineThickness(2);
        Graphics.DrawCircle("line", 100, 300, 50);
        Graphics.DrawCircle("fill", 225, 300, 50);
        
        Graphics.SetColor(255, 255, 255);
        Graphics.SetLineThickness(2);
        Graphics.DrawPolygon("line", 50, 375, 50, 400, 100, 425, 150, 425);
        Graphics.DrawPolygon("fill", 175, 375, 175, 400, 225, 425, 275, 425);
      });
      
      Window.Show();
    }
    
    static void AudioExample()
    {
      Window.OnLoad(() =>
      {
        var audioObj = Resources.LoadAudio("example.wav");
        audioObj.SetLoop(true);
        audioObj.Play();
      });
      
      Window.Show();
    }
    
    static void PrintingExample()
    {
      // Text object for "Hello, World!" text
      TextObject textObj = null;
      
      // Callback for OnLoad event to load and initialize all things
      Window.OnLoad(() =>
      {
        var font = Resources.LoadFont("roboto.ttf", 30);
        textObj = Graphics.NewText(font, "Hello, World!");
      });      
      
      // On draw calls every frame to draw graphics on screen
      Window.OnDraw(() => Graphics.Draw(textObj, 50, 50));
      
      // Start gameloop (at 60 FPS by default)
      Window.Show();
      
      //Common.ExecuteScript("entry.py");
      //Common.ExecuteScript("print-example.py");
    }
  }
}