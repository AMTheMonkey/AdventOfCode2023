namespace AdventOfCode2023.Common

open System
open System.Text.RegularExpressions

module Log =
    let log x =
        printfn "%A" x
        x


module Regexp =
    // ParseRegex parses a regular expression and returns a list of the strings that match each group in
    // the regular expression.
    // List.tail is called to eliminate the first element in the list, which is the full matched expression,
    // since only the matches for each group are wanted.
    let (|ParseRegex|_|) regex str =
        let m = Regex(regex).Match(str)

        if m.Success then
            Some(List.tail [ for x in m.Groups -> x.Value ])
        else
            None

module Number =

    type Number =
        | One = 1
        | Two = 2
        | Three = 3
        | Four = 4
        | Five = 5
        | Six = 6
        | Seven = 7
        | Eight = 8
        | Nine = 9


    let tryParseNumber (valueToBeParse: string) =
        match valueToBeParse.ToUpperInvariant() with
        | "ONE" -> Some(Number.One)
        | "TWO" -> Some(Number.Two)
        | "THREE" -> Some(Number.Three)
        | "FOUR" -> Some(Number.Four)
        | "FIVE" -> Some(Number.Five)
        | "SIX" -> Some(Number.Six)
        | "SEVEN" -> Some(Number.Seven)
        | "EIGHT" -> Some(Number.Eight)
        | "NINE" -> Some(Number.Nine)
        | _ -> None



    let tryParseInt (str: string) =
        try
            str |> int |> Some
        with :? FormatException ->
            None

    let tryParseNumberOrInt str =
        match tryParseNumber str with
        | Some(x) -> x |> int |> Some
        | None ->
            match tryParseInt str with
            | Some(x) -> if x < 10 then Some(x) else None
            | None -> None

module String =

    let reverseString (str: string) : string =
        Seq.rev str
        |> List.ofSeq
        |> List.map (fun (elem) -> elem |> string)
        |> List.reduce (fun acc next -> acc + next)

    let tryFindPattern (pattern: string, str: string) : string option =
        if str.ToUpperInvariant().Contains(pattern.ToUpperInvariant()) then
            Some(pattern)
        else
            None


    let rec tryFindFirstPatternOccurence (patterns: string list, str: string) =
        let matched =
            List.filter
                (fun (pattern: string) -> str.ToUpperInvariant().StartsWith(pattern.ToUpperInvariant()))
                patterns

        if (matched.IsEmpty) then
            if (str.Length = 1) then
                None
            else
                tryFindFirstPatternOccurence (patterns, str.[1..])
        else
            Some(matched[0])




    let tryFindLastPatternOccurence (patterns: string list, str: string) =
        let reversedPatterns = patterns |> List.map reverseString

        let reversedStr = reverseString str

        let matched = tryFindFirstPatternOccurence (reversedPatterns, reversedStr)

        if (matched.IsSome) then
            reverseString matched.Value |> Some
        else
            None
