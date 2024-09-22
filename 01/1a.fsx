open System
open System.IO

Directory.SetCurrentDirectory(__SOURCE_DIRECTORY__)

let lines = File.ReadAllLines("input.txt")

let lineDigits line =
    let digits =
        line
        |> Seq.toList
        |> List.fold
            (fun digits curr ->
                if Char.IsDigit(curr) then curr::digits
                else digits)
            []
        |> List.rev
        |> List.map string

    Int32.Parse((List.head digits) + (List.last digits))

lines |> Array.map lineDigits |> Array.sum
