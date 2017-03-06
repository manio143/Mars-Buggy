module Game

open SadConsole.Consoles
open SadConsole.Game
open SadConsole

open Microsoft.Xna.Framework

let Nullable<'a when 'a:(new:unit->'a) and 'a:struct and 'a:>System.ValueType> (x:'a) = System.Nullable x

let editorFill (editor:SurfaceEditor) (fg:Color) (bg:Color) (glyph:int) =
    editor.Fill(Nullable fg, Nullable bg, Nullable glyph) |> ignore

let welcomeScreen = 
    let mainText = GameObject(Engine.DefaultFont)

    let textSurface = AnimatedTextSurface("default", 43, 14)
    let frame = textSurface.CreateFrame()
    let editor = SurfaceEditor(TextSurface(1, 1, Engine.DefaultFont))

    editor.TextSurface <- frame
    editorFill editor Color.OrangeRed Color.Transparent 0

    editor.Print(0, 0, "MM     MM     A     RRRRR      SSS ")
    editor.Print(0, 1, "M M   M M    A A    R    R    SS  S")
    editor.Print(0, 2, "M  M M  M   A   A   RRRRR     SS   ")
    editor.Print(0, 3, "M   M   M   AAAAA   R R        SSS ")
    editor.Print(0, 4, "M       M  A     A  R  R     S   SS")
    editor.Print(0, 5, "M       M A       A R   RR    SSSS ")
    editor.Print(0, 6, "                                   ")
    editor.Print(0, 7,  "BBBBBB   U     U   GGGGG    GGGGG   Y     Y")
    editor.Print(0, 8,  "B     B  U     U  G     G  G     G   Y   Y")
    editor.Print(0, 9,  "BBBBBB   U     U  G        G          Y Y")
    editor.Print(0, 10, "B     B  U     U  G   GGG  G   GGG     Y")
    editor.Print(0, 11, "B     B  U     U  G     G  G     G    Y")
    editor.Print(0, 12, "BBBBBB    UUUUU    GGGGG    GGGGG   YY")

    mainText.Animation <- textSurface
    mainText.Position <- Point(20, 4)
    
    mainText

type MainConsole(width, height) =
    inherit Console(width, height)

    override this.Update() = ()

    override this.Render() =
        welcomeScreen.Render()