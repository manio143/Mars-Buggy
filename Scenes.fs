module Scenes

open System.Threading.Tasks

open SadConsole
open SadConsole.Input
open SadConsole.GameHelpers

open Microsoft.Xna.Framework

open ConsoleObject


type SceneType = Start | Game | Help

type Scene = {
                ConsoleObjects: GameObject list
                Type: SceneType
             }

type SceneHandler = {
                        Update: System.TimeSpan -> Scene -> Scene
                        ProcessKeyboard: Keyboard -> Scene -> Scene
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
    type PlayerState = Running | Jumping | Fallen

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

    let mutable currentState = Running
    let mutable currentAnimationObject = onGroundBuggy

    let mutable speed = 500

    let setState newState =
        match newState with
        | Running ->
            currentState <- Running
            currentAnimationObject <- onGroundBuggy
        | Jumping ->
            currentState <- Jumping
            currentAnimationObject <- inAirBuggy
        | Fallen ->
            currentState <- Fallen
            currentAnimationObject <- fallenBuggy

    let sleep (miliseconds:int) = Async.AwaitTask(Task.Delay(miliseconds))

    let jump () = async {
        if currentState = Running then
            setState Jumping
            for i in 1..4 do
                currentAnimationObject.Position <- currentAnimationObject.Position + Point(0,1)
                do! sleep speed
        else
            ()
    }
    
    let getScene () = {ConsoleObjects = floor::(currentAnimationObject.Objects()); Type = Game}



let processSingleKey key func (keyInfo:Keyboard) scene = if keyInfo.IsKeyDown(key) then func() else scene

let newGame = processSingleKey Input.Keys.Space <| fun () -> GameScene.getScene()
let quit =  processSingleKey Input.Keys.Q <| fun () -> SadConsole.Game.Instance.Exit(); {ConsoleObjects = []; Type = Game}
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