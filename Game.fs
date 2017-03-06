module Game

open SadConsole.Consoles
open SadConsole.Game
open SadConsole

open ConsoleObject

open Microsoft.Xna.Framework

let welcomeScreen = 
    let mainText = ConsoleObject.create()

    let animation = 
        { Foreground = Color.OrangeRed; Background = Color.Transparent; Glyph = 0}
        |> ConsoleObject.loadAnimation (Data.loadAnim "welcomeScreen")

    mainText.Animation <- animation
    mainText.Position <- Point(20, 4)
    
    mainText

type MainConsole(width, height) =
    inherit Console(width, height)

    override this.Update() = ()

    override this.Render() =
        welcomeScreen.Render()