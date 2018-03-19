from OxyFramework import *
from System import Math

def onLoad():
  global image
  image = Oxy.Resources.LoadTexture("test")

def onUpdate(dt):
  pass

def onDraw():
  Oxy.Graphics.Translate(500, 400)
  Oxy.Graphics.Draw(image, 0, 0)