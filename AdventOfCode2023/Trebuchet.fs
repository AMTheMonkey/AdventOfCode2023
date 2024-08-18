namespace AdventOfCode2023

open AdventOfCode2023.Common.String
open AdventOfCode2023.Common.Number

module Trebuchet =

    type CalibrationValue = int
    type CalibrationDocument = CalibrationValue list


    let buildCalibrationDocument (entries: string list) : CalibrationDocument =
        let patterns =
            [ "1"
              "2"
              "3"
              "4"
              "5"
              "6"
              "7"
              "8"
              "9"
              "ONE"
              "TWO"
              "THREE"
              "FOUR"
              "FIVE"
              "SIX"
              "SEVEN"
              "EIGHT"
              "NINE" ]

        let occurences =
            entries
            |> List.map (fun (entry: string) ->
                (tryFindFirstPatternOccurence (patterns, entry), tryFindLastPatternOccurence (patterns, entry)))

        printfn "%A" occurences

        occurences
        |> List.choose (fun (occurence) ->
            let (first, last) = occurence

            if first.IsSome && last.IsSome then
                (tryParseNumberOrInt first.Value).Value * 10
                + (tryParseNumberOrInt last.Value).Value
                |> Some
            else
                None)






    let sumOfCalibrationValues (calibrationDocument: CalibrationDocument) = List.sum calibrationDocument


//main
// let fileToRead =
//     @"C:\Users\Mathieu\OneDrive\Documents\fsharp\AdventOfCode2023\trebuchet.txt"

// let lines = FileReader.readFile fileToRead |> List.ofSeq

// let calibrationDocument = Trebuchet.buildCalibrationDocument lines

// calibrationDocument.Length |> printfn "Length %A"

// for i in calibrationDocument do
//     printf "%A;" i

// calibrationDocument
// |> Trebuchet.sumOfCalibrationValues
// |> printfn "CalibrationValue : %A"
