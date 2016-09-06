using HackerNews.Models;

namespace HackerNews.Interfaces
{
    interface IValidator
    {
        bool Validate(HackerNewsItem item);
    }
}
