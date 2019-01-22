module Script

open Newtonsoft.Json

type Config = { age: int }

let json = "{\"age\": 12355}"

let a = JsonConvert.DeserializeObject<Config>(json)

printfn "%A" a