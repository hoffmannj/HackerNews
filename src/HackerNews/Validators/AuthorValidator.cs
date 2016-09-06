using HackerNews.Interfaces;
using HackerNews.Models;

namespace HackerNews.Validators
{
    class AuthorValidator : IValidator
    {
        public bool Validate(HackerNewsItem item)
        {
            return !string.IsNullOrEmpty(item.By) && item.By.Length <= 256;
        }
    }
}
