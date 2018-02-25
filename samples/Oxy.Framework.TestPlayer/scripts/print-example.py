from Oxy.Framework import *
import math

def onLoad():
    global textObj
    global fpsCounter
    
    font = Resources.LoadFont("roboto.ttf", 48)
    font2 = Resources.LoadFont("roboto.ttf", 12)
    
    textObj = Graphics.NewText(font, "Hello World OxyEngine!")
    fpsCounter = Graphics.NewText(font2)


timer = 0
def onUpdate(dt):
    global timer
    fpsCounter.SetText("FPS: {0}".format(1//dt))
    timer += dt

def onDraw():
    Graphics.Draw(fpsCounter, 10, 10)
    Graphics.Draw(textObj, 50, 150, math.sin(timer) * 10)

Window.OnLoad(onLoad)
Window.OnUpdate(onUpdate)
Window.OnDraw(onDraw)

Window.Show(60)