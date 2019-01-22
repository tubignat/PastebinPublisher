module TelegramApi

open Telegram.Bot
open Telegram.Bot.Types

let token = ""
let bot = TelegramBotClient(token)

let getUpdates offset = bot.GetUpdatesAsync(offset).Result |> Array.toList
let deleteMessage (msg: Message) = bot.DeleteMessageAsync(ChatId(msg.Chat.Id), msg.MessageId).Wait()

let answer (msg: Message) text = bot.SendTextMessageAsync(ChatId(msg.Chat.Id), text).Result.MessageId