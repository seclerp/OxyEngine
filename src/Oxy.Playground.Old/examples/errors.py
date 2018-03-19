from Oxy.Framework import *

########################################################
# Custom exception thrown (for debugging error window) #
########################################################
def run_errors_example():
    def onLoad():
        raise Exception('Exception example')

    Window.OnLoad(onLoad)

    Window.Show(60)