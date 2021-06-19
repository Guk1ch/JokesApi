using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Telegram.Bot;

    namespace Tg.bot
    {
    class Program
    {
        static void Main(string[] args)
        {

            TelegramBotClient bot = new TelegramBotClient("1884107228:AAHlM-81LFKVMKIFNbiuIjOJ9Qdalfg8x_s");

            bot.OnMessage += (s, arg) =>
            {
                Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Ты говоришь: {arg.Message.Text}");
            };

            bot.StartReceiving();

            Console.ReadKey();
        }
    }
}
