using System;
using HackerNews.Interfaces;
using HackerNews.Models;
using System.Collections.Generic;
using HackerNews.Helpers;

namespace HackerNews
{
    class ResultListCreator
    {
        private IItemValidator _validator;
        private IConverter _converter;
        private IApi _api;

        public ResultListCreator(IItemValidator validator, IConverter converter, IApi api)
        {
            AssertHelper.AssertArgumentNotNull(validator, "validator");
            AssertHelper.AssertArgumentNotNull(converter, "converter");
            AssertHelper.AssertArgumentNotNull(api, "api");

            _validator = validator;
            _converter = converter;
            _api = api;
        }

        public List<OutputItem> CreateResultList(List<int> inputList, int maxCount)
        {
            AssertHelper.AssertArgumentNotNull(inputList, "inputList");
            var result = new List<OutputItem>();
            try
            {
                var loader = new SlidingLoader<int, HackerNewsItem>(inputList, id => _api.GetItem(id).Result);
                while (result.Count < maxCount)
                {
                    var items = loader.GetWindowItems();

                    foreach (var item in items)
                    {
                        if (item == null || !_validator.ItemIsValid(item)) continue;
                        result.Add(_converter.Convert(item, result.Count + 1));
                        if (result.Count == maxCount) break;
                    }
                    if (result.Count == maxCount) break;

                    if (!loader.CanSlide()) break;
                    loader.Slide();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHelper.WriteErrorMessages("Couldn't create the result list", ex);
                result = null;
            }

            return result;
        }
    }
}
