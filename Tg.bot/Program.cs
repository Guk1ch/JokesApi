using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Telegram.Bot;
using System.Net;
using Newtonsoft.Json;

namespace Tg.bot
    {
    class Program
    {
        static void Main(string[] args)
        {

            var category = "jod";
            // other categories: jod, animal, blonde

            var lang = "en";
            string res = "";

            TelegramBotClient bot = new TelegramBotClient("1884107228:AAHlM-81LFKVMKIFNbiuIjOJ9Qdalfg8x_s");

            bot.OnMessage += (s, arg) =>
            {

                Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                var url = $"https://api.jokes.one/jod?category={category}&language={lang}&";
                
                bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Ты говоришь: {arg.Message.Text}");

                var request = WebRequest.Create(url);

                var response = request.GetResponse();
                var httpStatusCode = (response as HttpWebResponse).StatusCode;

                if (httpStatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine(httpStatusCode);
                    return;
                }

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    //Console.WriteLine(result);
                    var Jokes = JsonConvert.DeserializeObject<Root>(result);

                    res += Jokes.contents.jokes[0].joke.text;

                }
                bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Joke: {res}");

            };

            bot.StartReceiving();

            Console.ReadKey();
        }
    }
    public class Success
    {
        public int total { get; set; }
    }

    public class Joke2
    {
        public string title { get; set; }
        public string lang { get; set; }
        public string length { get; set; }
        public string clean { get; set; }
        public string racial { get; set; }
        public string id { get; set; }
        public string text { get; set; }
    }

    public class Joke
    {
        public string description { get; set; }
        public string language { get; set; }
        public string background { get; set; }
        public string category { get; set; }
        public string date { get; set; }
        public Joke2 joke { get; set; }
    }

    public class Contents
    {
        public List<Joke> jokes { get; set; }
        public string copyright { get; set; }
    }

    public class Root
    {
        public Success success { get; set; }
        public Contents contents { get; set; }
    }
}
