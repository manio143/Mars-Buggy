module Data

open System.Reflection
open System.IO

let assembly = Assembly.GetExecutingAssembly()

let openEmbeded name =
    assembly.GetManifestResourceStream(name)

let loadAnim name =
    use stream = openEmbeded (name + ".anim")
    use reader = new StreamReader(stream)
    reader.ReadToEnd().Split('\n')