open TelegramApi
open PastebinApi
open Telegram.Bot.Types

let executeSafe innerFunction errorMessage =
    try Some innerFunction
    with | :? System.AggregateException as ex -> printfn "Error. %s: %s" errorMessage ex.Message; None

let deleteMessageSafe (msg: Message) = executeSafe (deleteMessage msg) "Could not delete a message" |> ignore
let answerSafe (msg: Message) text = executeSafe (answer msg text) "Could not send a message to the chat" |> ignore
let pasteTestSafe text = 
    executeSafe (pasteText text) "Could not paste a text" 
    |> fun result -> match result with | None -> "Could not create a pastebin note. Try again"; | Some link -> link

let truncateString (text: string) count = 
    if text.Length < count
    then text
    else text.[0..count] + (text.Length - count |> sprintf "... (%i symbols truncated)")

let processUpdate (update:Update) =
    printfn "Handling a message %i: %s" update.Message.MessageId (truncateString update.Message.Text 100)

    deleteMessageSafe update.Message
    pasteTestSafe update.Message.Text |> answerSafe update.Message

    update.Id

let rec loop offset =
    let updates = getUpdates offset
    match updates with
    | [] -> loop offset
    | _ -> List.map processUpdate updates |> List.last |> fun x -> x + 1 |> loop

[<EntryPoint>]
let rec main argv = printfn "Start..."; loop 1; 0