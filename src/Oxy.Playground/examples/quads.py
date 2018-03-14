from Oxy.Framework import *

properties = {}
viewRect = {}


def onLoad():
    properties['pony'] = Resources.LoadTexture('examples/assets/test.jpg')
    properties['ponySource'] = Graphics.NewRect(0, 0, 1, 1)
    properties['ponyDest'] = Graphics.NewRect(50, 50, 500, 500)

    Graphics.SetLineThickness(2)
    viewRect['mini'] = Graphics.NewRect(600, 50, 150, 150)


def updateViewRect(mouseX, mouseY):
    size = 50 + 25 * Window.GetMouseWheel()
    viewRect['pixelRect'] = Graphics.NewRect(mouseX - size / 2, mouseY - size / 2, size, size)
    viewRect['uvRect'] = Graphics.NewRect((viewRect['pixelRect'].X - properties['ponyDest'].X) / properties['ponyDest'].Width, (viewRect['pixelRect'].Y - properties['ponyDest'].Y) / properties['ponyDest'].Height, viewRect['pixelRect'].Width / properties['ponyDest'].Width, viewRect['pixelRect'].Height / properties['ponyDest'].Height)


def onUpdate(dt):
    updateViewRect(Window.GetCursorX(), Window.GetCursorY())

def onDraw():
    Graphics.Draw(properties['pony'], properties['ponySource'], properties['ponyDest'])
    Graphics.DrawRectangle('line', viewRect['pixelRect'])
    Graphics.Draw(properties['pony'], viewRect['uvRect'], viewRect['mini'])
