using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GiphyApi.Models
{
    public class Gif
    {
        public string url { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string size { get; set; }
        public string frames { get; set; }
        public string mp4 { get; set; }
        public string mp4_size { get; set; }
        public string webp { get; set; }
        public string webp_size { get; set; }
        public static List<Gif> listOfGifs;
        public static List<Gif> GetGif(string search, string limit)
        {
            var client = new RestClient("http://api.giphy.com/v1/gifs/");
            var request = new RestRequest("search?q=" + search + "&api_key=" + EnvironmentVariables.GiphyKey + "&limit=" + limit);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);

            Console.WriteLine(jsonResponse);
            List<Gif> gifList = new List<Gif> { };
            if(listOfGifs != null)
            {
                gifList = listOfGifs;

            }

            int cou;
            Int32.TryParse(limit, out cou);

            for(int i = 0; i <= cou - 1; i++)
            {
                Console.WriteLine(i);
                Gif gif = JsonConvert.DeserializeObject<Gif>(jsonResponse["data"][i]["images"]["original"].ToString());
                if(gif != null)
                {
                    gifList.Add(gif);
                }
            }
            return gifList;
        }
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
