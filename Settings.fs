module Settings

open System.IO
open Newtonsoft.Json

type Settings =
    {ApiDestination:string
     ApiKey:string}

let settings =
    @"Settings\\client.json"
    |> File.ReadAllText
    |> JsonConvert.DeserializeObject<Settings>