using System;
using System.Net;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class LoadFromWeb
    {
        public string Categories(string url)
        {
            string results;
            string exceptionMessage = "Categories could not be fetched.";

            using (WebClient client = new WebClient())
            {
                try
                {
                    results = client.DownloadString(url);
                }
                catch (WebException e)
                {
                    results = exceptionMessage;
                }
            }
            return results;
        }

        public Tuple<string, string> Identity (string url)
        {
            string results;

            using (WebClient client = new WebClient())
            {
                try
                {
                    results = client.DownloadString(url);
                    var obj = JsonConvert.DeserializeObject<dynamic>(results);
                    return Tuple.Create(((string)obj.name), ((string)obj.surname));
                }
                catch (WebException e)
                {
                    Console.WriteLine("Unfortunately, a random name could not be fetched.");
                    Console.WriteLine("Default name chosen.");
                    return Tuple.Create("Chuck", "Norris");
                }
            }
        }

        public string Joke (string url)
        {
            string results;
            string exceptionMessage = "Chuck Norris roundhouse kicked the joke too far away for the program to fetch.";

            using (WebClient client = new WebClient())
            {
                try
                {
                    results = client.DownloadString(url);
                    var obj = JsonConvert.DeserializeObject<dynamic>(results);
                    return (string)obj.value;
                }
                catch (WebException e)
                {
                    return exceptionMessage;
                }
            }
        }
    }
}