using HackerNews.Models;

namespace HackerNews.Interfaces
{
    interface IItemValidator
    {
        bool ItemIsValid(HackerNewsItem item);
    }
}
