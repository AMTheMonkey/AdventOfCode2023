namespace AdventOfCode2023

open AdventOfCode2023.FileReader

module Cube =

    type Record =
        { RedCube: int option
          GreenCube: int option
          BlueCube: int option }

    type Game = { Id: int; Records: List<Record> }

    let parseRegexInt pattern str =
        match str with
        | Common.Regexp.ParseRegex pattern integer -> Common.Number.tryParseInt integer[0]
        | _ -> None

    let parseGameId (str: string) = parseRegexInt "(\d+)$" str
    let parseRedCube (str: string) = parseRegexInt "(\d+) red" str
    let parseGreenCube (str: string) = parseRegexInt "(\d+) green" str
    let parseBlueCube (str: string) = parseRegexInt "(\d+) blue" str


    let parseRecord (entry: string) : Record =
        { RedCube = parseRedCube entry
          BlueCube = parseBlueCube entry
          GreenCube = parseGreenCube entry }


    let parseGame (entry: string) : Game =
        let gameEntry = entry.Split(':')
        let gameStr = gameEntry[0]
        let recordsStr = gameEntry[1].Split(';')
        let game = parseGameId gameStr
        let records = Array.map parseRecord recordsStr |> List.ofArray

        { Id = game.Value; Records = records }


    let parseAllGames () =
        readFile cubeData |> List.ofSeq |> List.map parseGame

    let validateGame (game: Game) (bag: Record) : bool =
        let invalidRecords =
            List.filter
                (fun (record: Record) ->
                    record.BlueCube > bag.BlueCube
                    || record.GreenCube > bag.GreenCube
                    || record.RedCube > bag.RedCube)
                game.Records

        invalidRecords.IsEmpty

    let sumGameId (games: Game list) =
        List.map (fun (game: Game) -> game.Id) games |> List.sum

    let firstPartConfiguration: Record =
        { BlueCube = Some(14)
          GreenCube = Some(13)
          RedCube = Some(12) }

    let solveFirstPart () =
        parseAllGames ()
        |> List.filter (fun game -> validateGame game firstPartConfiguration)
        |> Common.Log.log
        |> sumGameId
