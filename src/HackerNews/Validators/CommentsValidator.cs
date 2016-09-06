using HackerNews.Interfaces;
using HackerNews.Models;

namespace HackerNews.Validators
{
    class CommentsValidator : IValidator
    {
        public bool Validate(HackerNewsItem item)
        {
            if (!item.Descendants.HasValue) return false;
            return item.Descendants.Value >= 0;
        }
    }
}
