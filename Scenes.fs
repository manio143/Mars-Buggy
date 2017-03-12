module Scenes

open SadConsole.Consoles
open SadConsole.Game
open SadConsole.Input
open SadConsole

open Microsoft.Xna.Framework

open ConsoleObject


type SceneType = Start | Game

type Scene = {
                ConsoleObjects: GameObject list
                Type: SceneType
             }

let welcomeScreen = 
    createWithAnimationFromFile 20 4 "welcomeScreen"
    <| {defaultSurface with Foreground = Color.OrangeRed}

let instructions =
    createWithAnimation 1 24 [|"Press [Space] to start, [Q] to quit, [H] for help."|]
    <| defaultSurface

let startScene = { Type = Start; ConsoleObjects = [welcomeScreen; instructions] }

let newGame() = { Type = Game; ConsoleObjects = [] }

let update elapsedTime scene =
    match scene.Type with
    | Start -> scene
    | Game -> scene

let processKeyboard (keyboardInfo:KeyboardInfo) scene =
    match scene.Type with
    | Start -> 
            if keyboardInfo.IsKeyDown(Input.Keys.Space) then newGame()
            else scene
    | Game -> scene