# Using already defined modules
from Oxy.Framework import Window, Input

def update(dt):
  if Input.IsKeyPressed("escape"):
    Window.Exit()

# Registerning OnUpdat elistener (defined above)
Window.OnUpdate(update)

# This line must be last, because of thrad blocking
Window.Show(60.0)