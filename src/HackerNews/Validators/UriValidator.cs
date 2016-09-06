using System;
using HackerNews.Models;
using HackerNews.Interfaces;

namespace HackerNews.Validators
{
    class UriValidator : IValidator
    {
        public bool Validate(HackerNewsItem item)
        {
            if (string.IsNullOrEmpty(item.Url)) return false;
            return Uri.IsWellFormedUriString(item.Url, UriKind.Absolute);
        }
    }
}
