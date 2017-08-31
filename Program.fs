open System
open Client
open Newtonsoft.Json
open System.Collections.Generic

[<EntryPoint>]
let main argv =
    let rest = RestClient()
    let result = rest.GetLanguages()
    let data = JsonConvert.DeserializeObject<IDictionary<string,Object>>(snd result)
    data
    |> Seq.iter (fun x -> printfn "%s: %A" x.Key x.Value)
    0 // return an integer exit code
