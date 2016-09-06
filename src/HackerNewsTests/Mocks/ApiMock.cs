using HackerNews.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.Models;

namespace HackerNewsTests.Mocks
{
    class ApiMock : IApi
    {
        public Func<List<int>> TopStoryIdsFunc { get; set; }
        public Func<int, HackerNewsItem> GetItemFunc { get; set; }

        public int TimeOut { get; set; }

        public ApiMock()
        {
            TimeOut = 5000;
            TopStoryIdsFunc = () => new List<int>();
            GetItemFunc = id => null;
        }

        public async Task<HackerNewsItem> GetItem(int itemId)
        {
            if (GetItemFunc == null) throw new NotImplementedException();
            return await Task.Factory.StartNew(() => GetItemFunc(itemId));
        }

        public async Task<List<int>> GetTopStoryIds()
        {
            if (TopStoryIdsFunc == null) throw new NotImplementedException();
            return await Task.Factory.StartNew(() => TopStoryIdsFunc());
        }
    }
}
