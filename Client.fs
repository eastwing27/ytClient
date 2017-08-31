module Client

open System.Text
open System.Net
open System.Net.Http
open Settings

type RestClient () =
    let _server = settings.ApiDestination
    let _apikey = settings.ApiKey

    let interprete (response:HttpResponseMessage)=
        (
            response.StatusCode, 
            if response.IsSuccessStatusCode then 
               response.Content.ReadAsStringAsync().Result
            else
               response.ReasonPhrase
        )

    let flatten set =
        if Seq.isEmpty set then
            ""
        else
            set |> Seq.reduce (fun acc item -> acc + "&" + item)

    let makeUri route options =
        _server + "/" + 
        route + 
        "?key=" + _apikey + 
        flatten options
        
    let client = new HttpClient()

    let Post route body =
        client.PostAsync((route, []) ||> makeUri, new StringContent(body, Encoding.UTF8, "application/json")).Result
        |> interprete

    let Put route body =
        client.PutAsync((route, []) ||> makeUri, new StringContent(body, Encoding.UTF8, "application/json")).Result
        |> interprete

    let Get route options =
        client.GetAsync((route, options) ||> makeUri).Result
        |> interprete

    let Delete route =
        client.DeleteAsync((route, []) ||> makeUri).Result
        |> interprete

    member this.GetLanguages() =
        Get "getLangs" []