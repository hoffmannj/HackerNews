using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNewsTests
{
    class Creator
    {
        private static readonly Random _random = new Random();

        public static HackerNews.Models.HackerNewsItem CreateRandomItem(int id)
        {
            var descendants = _random.Next(10);
            var kids = new List<int>(Enumerable.Range(0, descendants).Select(n => _random.Next(999) + 10000));
            return new HackerNews.Models.HackerNewsItem
            {
                By = "test",
                Descendants = descendants,
                Id = id,
                Kids = kids,
                Score = _random.Next(70) + 1,
                Time = 11759485 + _random.Next(10000),
                Title = "Test story",
                Type = "story",
                Url = "http://nothing.com"
            };
        }
    }
}
