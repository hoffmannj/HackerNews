using HackerNews.Interfaces;
using HackerNews.Models;

namespace HackerNews.Validators
{
    class PointsValidator : IValidator
    {
        public bool Validate(HackerNewsItem item)
        {
            if (!item.Score.HasValue) return false;
            return item.Score.Value >= 0;
        }
    }
}
