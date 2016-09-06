using HackerNews.Interfaces;
using System;
using HackerNews.Models;

namespace HackerNewsTests.Mocks
{
    class ValidatorMock : IItemValidator
    {
        public Func<HackerNewsItem, bool> ValidatorFunc { get; set; }

        public ValidatorMock()
        {
            ValidatorFunc = item => true;
        }

        public bool ItemIsValid(HackerNewsItem item)
        {
            if (ValidatorFunc == null) throw new NotImplementedException();
            return ValidatorFunc(item);
        }
    }
}
