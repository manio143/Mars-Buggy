module ConsoleObject

open SadConsole.Consoles
open SadConsole.Game
open SadConsole

open Microsoft.Xna.Framework


let animationFrameSeparator = "𐆀"

type Surface = {
                Foreground: Color
                Background: Color
                Glyph: int
               }

let defaultSurface = { Foreground = Color.White; Background = Color.Transparent; Glyph = 0}

let editorFill (editor:SurfaceEditor) surface =
    editor.Fill(System.Nullable surface.Foreground,
                System.Nullable surface.Background,
                System.Nullable surface.Glyph) |> ignore

let create () = GameObject(Engine.DefaultFont)

let animationWithName name width height = AnimatedTextSurface(name, width, height)
let animation = animationWithName "default"

let addFrame (animation:AnimatedTextSurface) = animation.CreateFrame()

let editor frame = SurfaceEditor(frame)

let loadAnimation (text:string array) surface =
    let width = text |> Array.map (fun line -> line.Length) |> Array.max
    let height =
        let rec heightCount pos h current =
            if pos >= text.Length then (max h current)
            else 
                if text.[pos] = animationFrameSeparator then
                        heightCount (pos + 1) (max h current) 1
                else
                        heightCount (pos + 1) h (current + 1)
        heightCount 0 0 1
    let anim = animation width height
    let rec processText pos =
        let edit = addFrame anim |> editor
        do editorFill edit surface
        let rec fillFrame pos line =
            if pos >= text.Length then None
            else
                if text.[pos] = animationFrameSeparator then Some (pos + 1)
                else
                    do edit.Print(0, line, text.[pos])
                    fillFrame (pos + 1) (line + 1)
        match fillFrame pos 0 with
        | None -> ()
        | Some npos -> processText npos
    do processText 0
    anim


let createWithAnimation x y text surface =
     let entity = create ()
     entity.Animation <- loadAnimation text surface
     entity.Position <- Point(x, y)
     entity

let createWithAnimationFromFile x y animName surface =
    createWithAnimation x y (Data.loadAnim animName) surface