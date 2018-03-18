from OxyFramework import *

def onLoad():
  global image
  image = Oxy.Resources.LoadTexture("test")

def onUpdate(dt):
  pass

def onDraw():
  Oxy.Graphics.Draw(image, 50, 50)
  Oxy.Graphics.Translate(100, 100)
  Oxy.Graphics.Draw(image, 50, 50)