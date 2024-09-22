open System
open System.IO

Directory.SetCurrentDirectory(__SOURCE_DIRECTORY__)

let lines = File.ReadAllLines("input.txt")

let digitMap =
    Map.ofList
        [ ("one", "1")
          ("two", "2")
          ("three", "3")
          ("four", "4")
          ("five", "5")
          ("six", "6")
          ("seven", "7")
          ("eight", "8")
          ("nine", "9") ]

let lineDigits line =
    let digits =
        line
        |> Seq.toList
        |> List.fold
            (fun (digits, buffer) curr ->
                if Char.IsDigit(curr) then
                    (string curr :: digits, "")
                else
                    digitMap
                    |> Map.keys
                    |> List.ofSeq
                    |> List.tryPick (fun name ->
                        if (buffer + (string curr)).EndsWith(name) then
                            digitMap |> Map.tryFind name
                        else
                            None)
                    |> Option.map (fun digit -> (digit :: digits, buffer + (string curr)))
                    |> Option.defaultValue (digits, buffer + (string curr)))
            ([], "")
        |> fst
        |> List.rev

    Int32.Parse((List.head digits) + (List.last digits))

lines |> Array.map lineDigits |> Array.sum
