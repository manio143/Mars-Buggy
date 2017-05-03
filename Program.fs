open SadConsole

open Microsoft.Xna.Framework

open Game

[<EntryPoint>]
let main argv = 
    Game.Create("IBM.font", 80, 25)

    let init() =
        let console = new MainConsole(80, 25)
        Global.CurrentScreen <- console
        Global.FocusedConsoles.Set console

    Game.OnInitialize <- new System.Action(init)

    Game.Instance.Run()
    0 // return an integer exit code
