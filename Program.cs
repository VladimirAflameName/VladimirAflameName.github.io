using MihaZupan; //proxy
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ConsoleApp2
{
    class Program
    {
        private static ITelegramBotClient botClient;
        static void Main(string[] args)
        {
			//бот будет выводить день недели в ответ на любое сообщение
            var proxy = new HttpToSocks5Proxy("207.97.174.134", 1080);
            botClient = new TelegramBotClient("1400277860:AAFJJcFooQm1LlcZaH_SWoeeGz3EZVUJY1k", proxy);
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"name: {me.FirstName}"); //проверка подключения
          
            botClient.OnMessage += BotMessage; 
            botClient.StartReceiving();
            Console.ReadKey();
            botClient.StopReceiving();
        }

        private async static void BotMessage(object sender, MessageEventArgs e)
        {
            var text = e.Message.Text;
            if (text == null)
                return;
            DateTime thisDay = DateTime.Today;
            await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: $"Сегодня {thisDay.ToString("dddd")}").ConfigureAwait(false);
        }
    }
}
