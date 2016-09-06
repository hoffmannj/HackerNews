using HackerNews.Interfaces;
using HackerNews.Models;

namespace HackerNews.Validators
{
    class TitleValidator : IValidator
    {
        public bool Validate(HackerNewsItem item)
        {
            return !string.IsNullOrEmpty(item.Title) && item.Title.Length <= 256;
        }
    }
}
