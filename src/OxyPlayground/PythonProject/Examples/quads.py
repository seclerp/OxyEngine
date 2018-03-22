from OxyEngine import *

properties = {}
viewRect = {}


def onLoad(s, args):
    properties['pony'] = Oxy.Resources.LoadTexture('test')
    properties['ponySource'] = Oxy.Graphics.NewRect(0, 0, properties['pony'].Width, properties['pony'].Height)
    properties['ponyDest'] = Oxy.Graphics.NewRect(50, 50, 500, 500)

    Oxy.Graphics.SetLineWidth(2)
    viewRect['mini'] = Oxy.Graphics.NewRect(600, 50, 150, 150)


def updateViewRect(mouseX, mouseY):
    size = 50 + Oxy.Input.GetMouseWheel()
    viewRect['pixelRect'] = Oxy.Graphics.NewRect(mouseX - size / 2, mouseY - size / 2, size, size)
    viewRect['uvRect'] = Oxy.Graphics.NewRect(viewRect['pixelRect'].X, viewRect['pixelRect'].Y, viewRect['pixelRect'].Width, viewRect['pixelRect'].Height)


def onUpdate(s, args):
    updateViewRect(Oxy.Input.GetCursorPosition().X, Oxy.Input.GetCursorPosition().Y)

def onDraw(s, args):
    Oxy.Graphics.Draw(properties['pony'], properties['ponySource'], properties['ponyDest'])
    Oxy.Graphics.Rectangle('line', viewRect['pixelRect'])
    Oxy.Graphics.Draw(properties['pony'], viewRect['uvRect'], viewRect['mini'])
