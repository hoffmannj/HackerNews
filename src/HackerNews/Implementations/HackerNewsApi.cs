using HackerNews.Interfaces;
using HackerNews.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Implementations
{
    class HackerNewsApi : IApi
    {
        private const string TOPSTORIESURL = "https://hacker-news.firebaseio.com/v0/topstories.json";
        private const string GETITEMURL = "https://hacker-news.firebaseio.com/v0/item/{0}.json";

        public int TimeOut { get; set; }

        public HackerNewsApi()
        {
            //5 seconds timeout should be enough
            TimeOut = 5000;
        }

        public async Task<List<int>> GetTopStoryIds()
        {
            var content = await GetAsync(TOPSTORIESURL);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(content);
        }

        public async Task<HackerNewsItem> GetItem(int id)
        {
            var itemUrl = string.Format(GETITEMURL, id);
            var content = await GetAsync(itemUrl);
            var item = Newtonsoft.Json.JsonConvert.DeserializeObject<HackerNewsItem>(content);
            if (item != null)
            {
                item.Descendants = item.Descendants ?? 0;
                item.Score = item.Score ?? 0;
                item.Kids = item.Kids ?? new List<int>();
            }
            return item;
        }

        private async Task<string> GetAsync(string uri)
        {
            using (var source = new CancellationTokenSource(TimeOut))
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseContentRead, source.Token);
                //will throw an exception if not successful
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
