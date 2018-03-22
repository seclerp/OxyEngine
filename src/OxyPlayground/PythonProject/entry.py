from Examples.quads import *

Oxy.Events.Global.StartListening("load", onLoad)
Oxy.Events.Global.StartListening("update", onUpdate)
Oxy.Events.Global.StartListening("draw", onDraw)