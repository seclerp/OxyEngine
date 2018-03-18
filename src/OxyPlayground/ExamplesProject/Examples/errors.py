from Oxy.Framework import *

########################################################
# Custom exception thrown (for debugging error window) #
########################################################

def onLoad():
	raise Exception('Exception example')

Window.OnLoad(onLoad)

Window.Show(60)