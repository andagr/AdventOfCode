open System
open System.IO

Directory.SetCurrentDirectory(__SOURCE_DIRECTORY__)

let lines = File.ReadAllLines("input.txt")

type Cube =
    | Blue of int
    | Red of int
    | Green of int

type Game = { Number: int; Cubes: Cube list }

let inputCubes = [ Blue 14; Green 13; Red 12 ]

let parseGame (line: string) =
    let [| gameLine; cubesLine |] = line.Split(':')
    let gameNbr = gameLine.Split(' ') |> Array.last |> string |> Int32.Parse

    let cubes =
        cubesLine.Split([| ';'; ',' |], StringSplitOptions.TrimEntries ||| StringSplitOptions.RemoveEmptyEntries)
        |> Array.map (fun cubeDesc ->
            let [| count; color |] = cubeDesc.Split(' ')
            let count = Int32.Parse count

            match color with
            | "blue" -> Blue count
            | "red" -> Red count
            | "green" -> Green count)
        |> Array.toList

    { Number = gameNbr; Cubes = cubes }

let isPossibleWith (cubes: Cube list) (game: Game) =
    game.Cubes
    |> List.exists (fun gameCube ->
        match gameCube with
        | Blue gameCount ->
            cubes
            |> List.exists (function
                | Blue inputCount -> gameCount > inputCount
                | _ -> false)
        | Red gameCount ->
            cubes
            |> List.exists (function
                | Red inputCount -> gameCount > inputCount
                | _ -> false)
        | Green gameCount ->
            cubes
            |> List.exists (function
                | Green inputCount -> gameCount > inputCount
                | _ -> false))
    |> not

lines
|> Array.map parseGame
|> Array.filter (isPossibleWith inputCubes)
|> Array.sumBy (_.Number)
