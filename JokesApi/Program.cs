//Artemev 220
// API JOKES

using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Api_Joke
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var category = "knock-knock";
            var lang = "en";
            var url = $"https://api.jokes.one/jod?category={category}&language={lang}&";

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

                Console.WriteLine(Jokes.contents.jokes[0].joke.text);
            }

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
