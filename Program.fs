// Learn more about F# at http://fsharp.org

open System
open Client

[<EntryPoint>]
let main argv =
    let rest = RestClient "https://geocode-maps.yandex.ru/1.x"
    let result = rest.Get "?geocode=Хабаровск,+Шеронова+улица,+дом+7&format=json"
    printfn "%A" result
    0 // return an integer exit code
