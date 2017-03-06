module Data

open System.Reflection
open System.IO

let assembly = Assembly.GetExecutingAssembly()

//TODO:Remove if not required on Windows
//do printfn "%A" (assembly.GetManifestResourceNames())
//let defaultNamespace = "MarsBuggy"

let openEmbeded name = 
    //let fileName = defaultNamespace + ".Content." + name
    assembly.GetManifestResourceStream(name)

let loadAnim name =
    use stream = openEmbeded (name + ".anim")
    use reader = new StreamReader(stream)
    reader.ReadToEnd().Split('\n')