open SadConsole.Consoles
open Microsoft.Xna.Framework

open Game

[<EntryPoint>]
let main argv = 
    SadConsole.Engine.Initialize("IBM.font", 80, 25);

    SadConsole.Engine.EngineStart.Add (fun _ ->
            SadConsole.Engine.ConsoleRenderStack.Clear()
            let console = MyConsole(80, 25)
            SadConsole.Engine.ConsoleRenderStack.Add(console)
            console.Print(1,2, "Whhoa"))

    SadConsole.Engine.Run()
    0 // return an integer exit code

