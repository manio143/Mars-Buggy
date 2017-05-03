module Game

open SadConsole


type MainConsole(width, height) =
    inherit Console(width, height)

    let mutable Scene = Scenes.StartScene.scene

    override this.ProcessKeyboard keyInfo =
        Scene <- Scenes.processKeyboard keyInfo Scene
        true

    override this.Update delta =
        Scene <- Scenes.update delta Scene

    override this.Draw delta =
        Scene.ConsoleObjects
        |> List.iter (fun entity -> entity.Draw(delta))