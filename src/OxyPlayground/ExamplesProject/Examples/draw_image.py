from OxyEngine import *
from System import Math

timer = 0;

def onLoad(s, args):
  global image
  image = Oxy.Resources.LoadTexture("test")

def onUpdate(s, args):
  global timer
  timer += args.DeltaTime
  pass

def onDraw(s, args):
  Oxy.Graphics.Translate(500, 400)
  Oxy.Graphics.Draw(image, 0, 0, image.Width / 2, image.Height / 2, Math.Sin(timer) / 2)