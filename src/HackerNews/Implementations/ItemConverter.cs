using HackerNews.Interfaces;
using HackerNews.Models;
using HackerNews.Helpers;

namespace HackerNews.Implementations
{
    class ItemConverter : IConverter
    {
        public OutputItem Convert(HackerNewsItem item, int rank)
        {
            AssertHelper.AssertArgumentNotNull(item, "item");
            AssertHelper.AssertFieldNotNull(item.Descendants, "Descendants");
            AssertHelper.AssertFieldNotNull(item.Score, "Score");
            return new OutputItem
            {
                Author = item.By,
                Comments = item.Descendants.Value,
                Points = item.Score.Value,
                Title = item.Title,
                Uri = item.Url,
                Rank = rank
            };
        }
    }
}
