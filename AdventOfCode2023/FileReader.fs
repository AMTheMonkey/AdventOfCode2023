namespace AdventOfCode2023

open System.IO

module FileReader =

    [<Literal>]
    let baseFolder = @"C:\Users\Mathieu\OneDrive\Documents\fsharp\AdventOfCode2023\"

    [<Literal>]
    let trebuchetData = baseFolder + "trebuchet.txt"

    [<Literal>]
    let cubeData = baseFolder + "cube_games.txt"

    let readFile path = File.ReadLines path

    let parseCsvFile path delimiter =
        readFile path
        |> List.ofSeq
        |> List.map (fun (line: string) -> line.Split(delimiter))
