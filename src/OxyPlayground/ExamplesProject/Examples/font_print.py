from Oxy.Framework import *
from System import Math

##########################################
# Text printing with custom font example #
##########################################
def onLoad():
    global textObj
    global timer
    timer = 0

    font = Resources.LoadBitmapFont("examples/assets/font_example.png", " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
    textObj = Graphics.NewText(font, "Hello \nWorld \nOxyEngine")
    Graphics.SetBackgroundColor(0, 0, 0);


def onUpdate(dt):
    global timer
    timer += dt

def onDraw():
    Graphics.Draw(textObj, 50, 150, 100, 20, Math.Sin(timer) * 10)

