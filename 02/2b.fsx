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

let minNeededCubesPower cubes =
    cubes
    |> List.fold
        (fun (blue, red, green) cube ->
            match cube with
            | Blue count -> (max blue count , red, green)
            | Red count -> (blue, max red count, green)
            | Green count -> (blue, red, max green count))
        (0, 0, 0)
    |> fun (blue, red, green) -> blue * red * green

lines
|> Array.map parseGame
|> Array.map (_.Cubes >> minNeededCubesPower)
|> Array.sum
