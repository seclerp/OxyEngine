from Oxy.Framework import *
from System import Random

objects = []

def onLoad():
    global texture, texture2, fpsCounter, random

    texture = Graphics.NewTexture(Window.GetWidth(), Window.GetHeight())
    texture2 = Resources.LoadTexture('examples/assets/test.jpg')
    fpsCounter = Graphics.NewText("FPS: ")

    random = Random()

    Graphics.SetRenderTexture(texture)

    for i in xrange(0, 99999):
        objects.append({})
        objects[i]["dest"] = Graphics.NewRect(50 + random.Next(0, 1000), 50 + random.Next(0, 1000), random.Next(10, 300), random.Next(10, 300))
        objects[i]["source"] = Graphics.NewRect(0, 0, 1, 1)
        Graphics.Draw(texture2, objects[i]["source"], objects[i]["dest"])
        
    Graphics.SetRenderTexture()

    Graphics.SetBackgroundColor(50, 50, 50)


def onUpdate(dt):
    fpsCounter.SetText("FPS: {}\nRender time: {} ms.\nCount of objects: {}".format(Window.GetFPS(), Window.GetRenderTime(), len(objects)))


def onDraw():
    Graphics.Draw(texture)
    Graphics.SetColor(75, 75, 75)
    Graphics.DrawRectangle('fill', 0, 0, 400, 100)
    Graphics.SetColor(255, 255, 255)
    Graphics.Draw(fpsCounter, 50, 20)

    pass