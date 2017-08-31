open System
open Client
open System.Collections.Generic

[<EntryPoint>]
let main argv =
    let rest = RestClient()
    let result = Console.ReadLine() |> rest.Translate("ru")
    printfn "%s:" result.Lang
    result.Text |> Array.iter (printfn "%s")
    // let data = JsonConvert.DeserializeObject<IDictionary<string,Object>>(snd result)
    // data
    // |> Seq.iter (fun x -> printfn "%s: %A" x.Key x.Value)
    0 // return an integer exit code
