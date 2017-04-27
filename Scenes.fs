module Scenes

open SadConsole.Consoles
open SadConsole.Game
open SadConsole.Input
open SadConsole

open Microsoft.Xna.Framework

open ConsoleObject


type SceneType = Start | Game | Help

type Scene = {
                ConsoleObjects: GameObject list
                Type: SceneType
             }

type SceneHandler = {
                        Update: float -> Scene -> Scene
                        ProcessKeyboard: KeyboardInfo -> Scene -> Scene
                    }



module StartScene =
    let welcomeScreen = 
        createWithAnimationFromFile 20 4 "welcomeScreen"
        <| {defaultSurface with Foreground = Color.OrangeRed}

    let instructions =
        createWithAnimation 1 24 [|"Press [Space] to start, [Q] to quit, [H] for help."|]
        <| defaultSurface

    let floor =
        createWithAnimation 0 22 [|String.replicate 80 "#"; String.replicate 80 "#"|]
        <| defaultSurface

    let scene = { Type = Start; ConsoleObjects = [welcomeScreen; instructions; floor] }

module HelpScene =
    let helpInfo =
        createWithAnimationFromFile 1 1 "Help"
        <| defaultSurface

    let instructions =
        createWithAnimation 1 24 [|"Press [Esc] to go back."|]
        <| defaultSurface

    let scene = { Type = Help; ConsoleObjects = [helpInfo; instructions] }

module GameScene =
    let wheels = createWithAnimationFromFile 0 1 "wheels" defaultSurface
    do wheels.Animation.AnimationDuration <- 2.0f
    do wheels.Animation.Repeat <- true
    do wheels.Animation.Start()

    let onGroundBuggy = new MultiObject([
                                            createWithAnimationFromFile 0 0 "buggy" defaultSurface
                                            wheels
                                        ])
    do onGroundBuggy.Position <- Point(70, 20)
    let inAirBuggy = new MultiObject([
                                        createWithAnimationFromFile 0 0 "jump" defaultSurface
                                        wheels
                                     ])
    let fallenBuggy = new MultiObject([
                                        createWithAnimationFromFile 0 0 "fall" defaultSurface
                                      ])
    let floor =
        createWithAnimation 0 23 [|String.replicate 80 "#"|]
        <| defaultSurface
    let scene = {ConsoleObjects = floor::(onGroundBuggy.Objects()); Type = Game}



let processSingleKey key func (keyInfo:KeyboardInfo) scene = if keyInfo.IsKeyDown(key) then func() else scene

let newGame = processSingleKey Input.Keys.Space <| fun () -> GameScene.scene
let quit =  processSingleKey Input.Keys.Q <| fun () -> Engine.MonoGameInstance.Exit(); {ConsoleObjects = []; Type = Game}
let help = processSingleKey Input.Keys.H <| fun () -> HelpScene.scene

let startSceneHandler = {
                            Update = fun _ scene -> scene
                            ProcessKeyboard = fun kInfo -> 
                                    newGame kInfo
                                    >> quit kInfo
                                    >> help kInfo
                        }
                                      
let goToStart = processSingleKey Input.Keys.Escape <| fun () -> StartScene.scene

let helpSceneHandler = {
                            Update = fun _ scene -> scene
                            ProcessKeyboard = goToStart
                       }


let update elapsedTime scene =
    match scene.Type with
    | Start -> startSceneHandler.Update elapsedTime scene
    | Game -> scene
    | Help -> helpSceneHandler.Update elapsedTime scene

let processKeyboard keyboardInfo scene =
    match scene.Type with
    | Start -> startSceneHandler.ProcessKeyboard keyboardInfo scene
    | Game -> scene
    | Help -> helpSceneHandler.ProcessKeyboard keyboardInfo scene