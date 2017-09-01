namespace ytClient

module Client =

    open System.Text
    open System.Net
    open System.Net.Http
    open Settings
    open Newtonsoft.Json
    open DTO

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
                set 
                |> Seq.map (fun x -> "&"+x)
                |> String.concat ""

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

        member this.RetrieveResult<'T> response =
            if (fst response) <> HttpStatusCode.OK then
                failwith (snd response)
            else
                (snd response)
                |> JsonConvert.DeserializeObject<'T>

        member this.GetLanguages() =
            Get "getLangs" []
            |> this.RetrieveResult<LangList>

        member this.Detect text =
            Get "detect" ["text="+text]
            |> this.RetrieveResult<LangDTO>

        member this.Translate lang text =
            Get "translate" ["text="+text; "lang="+lang]
            |> this.RetrieveResult<Translation>