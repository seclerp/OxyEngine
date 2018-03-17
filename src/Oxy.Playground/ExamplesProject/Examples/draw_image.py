from Oxy.Framework import *

def onLoad():
	global image
	image = Resources.LoadTexture("test")

def onUpdate(dt):
	pass

def onDraw():
	Graphics.Draw(image, 50, 50)