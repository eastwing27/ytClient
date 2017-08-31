module DTO

open System.Collections.Generic

type LangList =
    {Dirs:seq<string>
     Langs:seq<string*string>}

type LangDTO =
    {Lang:string}

type Translation =
    {Lang:string
     Text:string[]}