using HackerNews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Interfaces
{
    interface IApi
    {
        int TimeOut { get; set; }
        Task<List<int>> GetTopStoryIds();
        Task<HackerNewsItem> GetItem(int itemId);
    }
}
