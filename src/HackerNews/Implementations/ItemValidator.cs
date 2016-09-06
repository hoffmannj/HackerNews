using HackerNews.Helpers;
using HackerNews.Interfaces;
using HackerNews.Models;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Implementations
{
    class ItemValidator : IItemValidator
    {
        private List<IValidator> _validators;

        public ItemValidator(IEnumerable<IValidator> validators)
        {
            AssertHelper.AssertArgumentNotNull(validators, "validators");
            _validators = new List<IValidator>(validators);
        }

        public bool ItemIsValid(HackerNewsItem item)
        {
            if (item == null) return false;
            return _validators.All(validator => validator.Validate(item));
        }
    }
}
