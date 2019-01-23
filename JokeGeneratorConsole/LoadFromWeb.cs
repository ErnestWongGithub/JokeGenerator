using System;
using System.Net;

namespace JokeGenerator
{
    public class LoadFromWeb
    {

        public string Categories(string url)
        {
            string results;
            using (WebClient client = new WebClient())
            {
                try
                {
                    results = client.DownloadString(url);
                }
                catch (WebException e)
                {
                    results = "Unfortunately, the categories could not be fetched.";
                }
            }
            return results;
        }
    }
}