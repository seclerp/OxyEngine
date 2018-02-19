# Using already defined modules
from Oxy.Framework import Window, Input

print "Hello from Python script"

def checkExitByKey(dt):
  if Input.IsKeyPressed("escape"):
    Window.Exit()

def printDt(dt):
  print "Delta time: ", dt

# Registerning OnUpdate listener (defined above)
Window.OnUpdate(checkExitByKey)
Window.OnUpdate(printDt)

# This line must be last, because of thread blocking
Window.Show(60.0)