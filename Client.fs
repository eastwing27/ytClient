module Client

open System.Text
open System.Net;
open System.Net.Http;

type RestClient (server:string) =
    let _server = server
    let interprete (response:HttpResponseMessage)=
        (response.StatusCode, 
         if response.IsSuccessStatusCode then 
             response.Content.ReadAsStringAsync().Result
         else
              response.ReasonPhrase)
        
    member private this.Client = new HttpClient()

    member this.Post route body =
        this.Client.PostAsync(_server+"/"+route, new StringContent(body, Encoding.UTF8, "application/json")).Result
        |> interprete

    member this.Put route body =
        this.Client.PutAsync(_server+"/"+route, new StringContent(body, Encoding.UTF8, "application/json")).Result
        |> interprete

    member this.Get route =
        this.Client.GetAsync(server+"/"+route).Result
        |> interprete

    member this.Delete route =
        this.Client.DeleteAsync(server+"/"+route).Result
        |> interprete