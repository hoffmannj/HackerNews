using HackerNews.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews
{
    /// <summary>
    /// Parallel generic data loader that loads data in batches (aka. windows)
    /// IMPORTANT: Type T used as key in a dictionary, so make sure GetHashCode() is properly implemented
    /// This class uses 4 as the default level of concurrency, and the default window size is 20.
    /// </summary>
    class SlidingLoader<T, U>
    {
        private List<T> _keyList;
        private int _windowStart;
        private int _windowSize;
        private Func<T, U> _loaderFunc;
        private ConcurrentDictionary<T, U> _windowItems;

        public SlidingLoader(List<T> keyList, Func<T, U> loaderFunc)
        {
            AssertHelper.AssertArgumentNotNull(keyList, "keyList");
            AssertHelper.AssertArgumentNotNull(loaderFunc, "loaderFunc");
            _keyList = new List<T>(keyList);
            _loaderFunc = loaderFunc;
            _windowSize = 20;
            _windowStart = 0;
            _windowItems = new ConcurrentDictionary<T, U>(4, keyList.Count);
            LoadWindow();
        }

        /// <summary>
        /// Loads data for the current window and stores the results in a dictionary
        /// so it can give back results in the correct order
        /// </summary>
        private void LoadWindow()
        {
            var toTransform = _keyList.Skip(_windowStart).Take(_windowSize).ToList();
            toTransform.AsParallel().ForAll(id =>
            {
                var item = _loaderFunc(id);
                _windowItems.AddOrUpdate(id, item, (key, oldValue) => item);
            });
        }

        public bool CanSlide()
        {
            return (_keyList.Count - _windowStart) > _windowSize;
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
        public List<U> GetWindowItems()
        {
            var result = new List<U>();
            for (int i = 0; i < _windowSize; ++i)
            {
                if (_windowStart + i >= _keyList.Count) break;
                U item = default(U);
                if (_windowItems.ContainsKey(_keyList[_windowStart + i]))
                {
                    item = _windowItems[_keyList[_windowStart + i]];
                }
                result.Add(item);
            }
            return result;
        }
    }
}
