using HackerNews.Models;

namespace HackerNews.Interfaces
{
    interface IConverter
    {
        OutputItem Convert(HackerNewsItem item, int rank);
    }
}
