module Game

open SadConsole.Consoles
open SadConsole


type MainConsole(width, height) =
    inherit Console(width, height)

    let mutable Scene = Scenes.startScene

    let processKeyboardAndUpdate =
        Scenes.processKeyboard (Engine.Keyboard)
        >> Scenes.update (Engine.GameTimeElapsedUpdate)

    override this.Update() =
        Scene <- processKeyboardAndUpdate Scene

    override this.Render() =
        Scene.ConsoleObjects
        |> List.iter (fun entity -> entity.Render())