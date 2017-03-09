module Game

open SadConsole.Consoles
open SadConsole.Game
open SadConsole

open ConsoleObject

open Microsoft.Xna.Framework

let welcomeScreen = 
    createWithAnimationFromFile 20 4 "welcomeScreen"
    <| {defaultSurface with Foreground = Color.OrangeRed}


type MainConsole(width, height) =
    inherit Console(width, height)

    override this.Update() = ()

    override this.Render() =
        welcomeScreen.Render()