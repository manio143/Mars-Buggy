module Game

open SadConsole.Consoles
open SadConsole.Game
open SadConsole.Input
open SadConsole

open ConsoleObject

open Microsoft.Xna.Framework

[<AbstractClass>]
type Scene() =
    abstract member ConsoleObjects: unit -> GameObject list
    abstract member Update: unit -> unit
    abstract member ProcessKeyboard: KeyboardInfo -> Scene option

    default this.Update() = ()

type StartScene() =
    inherit Scene()

    let welcomeScreen = 
        createWithAnimationFromFile 20 4 "welcomeScreen"
        <| {defaultSurface with Foreground = Color.OrangeRed}

    let instructions =
        createWithAnimation 1 24 [|"Press [Space] to start, [Q] to quit, [H] for help."|]
        <| defaultSurface

    override this.ConsoleObjects () = [welcomeScreen; instructions]
    override this.ProcessKeyboard keyInfo = None
        

type MainConsole(width, height) =
    inherit Console(width, height)

    member val Scene : Scene = StartScene() :> Scene
           with get, set

    override this.Update() = this.Scene.Update()

    override this.Render() =
        this.Scene.ConsoleObjects()
        |> List.iter (fun entity -> entity.Render())

    override this.ProcessKeyboard(keyboardInfo) = 
        match this.Scene.ProcessKeyboard(keyboardInfo) with
         | None -> ()
         | Some newScene -> this.Scene <- newScene
        true