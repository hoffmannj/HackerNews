using HackerNews.Helpers;
using HackerNews.Interfaces;
using HackerNews.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews
{
    /// <summary>
    /// Parallel data loader that loads data in batches (aka. windows)
    /// </summary>
    class SlidingLoader
    {
        private List<int> _idList;
        private int _windowStart;
        private int _windowSize;
        private ConcurrentDictionary<int, HackerNewsItem> _windowItems;
        private IApi _api;

        public SlidingLoader(IApi api, List<int> idList)
        {
            AssertHelper.AssertArgumentNotNull(api, "api");
            AssertHelper.AssertArgumentNotNull(idList, "idList");
            _api = api;
            _idList = new List<int>(idList);
            _windowSize = 20;
            _windowStart = 0;
            _windowItems = new ConcurrentDictionary<int, HackerNewsItem>(8, _windowSize);
            LoadWindow();
        }

        /// <summary>
        /// Loads data for the current window and stores the results in a dictionary
        /// so it can give back results in the correct order
        /// </summary>
        private void LoadWindow()
        {
            var toTransform = _idList.Skip(_windowStart).Take(_windowSize).ToList();
            toTransform.AsParallel().ForAll(id =>
            {
                var item = _api.GetItem(id).Result;
                _windowItems.AddOrUpdate(item.Id, item, (key, oldValue) => item);
            });
        }

        public bool CanSlide()
        {
            return (_idList.Count - _windowStart) > _windowSize;
        }

        /// <summary>
        /// "Slides" the window to the next batch of IDs
        /// </summary>
        public void Slide()
        {
            _windowStart += _windowSize;
            LoadWindow();
        }

        /// <summary>
        /// Returns with the data ordered by the IDs
        /// </summary>
        /// <returns>List of HackerNewsItem</returns>
        public List<HackerNewsItem> GetWindowItems()
        {
            var result = new List<HackerNewsItem>();
            for (int i = 0; i < _windowSize; ++i)
            {
                if (_windowStart + i >= _idList.Count) break;
                HackerNewsItem item = null;
                if (_windowItems.ContainsKey(_idList[_windowStart + i]))
                {
                    item = _windowItems[_idList[_windowStart + i]];
                }
                result.Add(item);
            }
            return result;
        }
    }
}
