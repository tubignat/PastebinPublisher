module PastebinApi

open Hopac
open HttpFs.Client
open Newtonsoft.Json

let private developerApiKey = ""

let private buildBodyInternal (apiKey: string) (code: string) = BodyForm [
        NameValue ("api_dev_key", apiKey)
        NameValue ("api_option", "paste")
        NameValue ("api_paste_code", code)
    ]

let private pasteInternal buildBody apiKey code = 
    Request.createUrl Post "https://pastebin.com/api/api_post.php" 
    |> Request.body (buildBody apiKey code) 
    |> Request.responseAsString
    |> run


let pasteText = pasteInternal buildBodyInternal developerApiKey