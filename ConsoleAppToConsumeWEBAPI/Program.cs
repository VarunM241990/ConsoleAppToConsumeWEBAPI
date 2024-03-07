using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppToConsumeWEBAPI
{
    public class Datum
    {
        public string name { get; set; }
        public string runtime_of_series { get; set; }
        public string certificate { get; set; }
        public string runtime_of_episodes { get; set; }
        public string genre { get; set; }
        public double imdb_rating { get; set; }
        public string overview { get; set; }
        public int no_of_votes { get; set; }
        public int id { get; set; }
    }

    public class Root
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Datum> data { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            GetData().Wait();

            Console.ReadLine();
        }

        static async Task GetData() 
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://jsonmock.hackerrank.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("/api/tvseries?page=1");

            Root root = null;

            if (response.IsSuccessStatusCode) 
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                //Console.WriteLine(responseBody);

                root = JsonConvert.DeserializeObject<Root>(responseBody);
            }

            foreach(var dt in root.data) 
            {
                Console.WriteLine("Name: " + dt.name + " Rating: " + dt.imdb_rating);
            }

        }
    }
}
