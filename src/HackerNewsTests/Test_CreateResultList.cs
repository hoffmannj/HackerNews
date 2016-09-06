using HackerNews;
using HackerNews.Implementations;
using HackerNewsTests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNewsTests
{
    [TestFixture]
    public class Test_CreateResultList
    {

        [Test]
        public void CreateResultList_Empty()
        {
            var validator = new ValidatorMock();
            var converter = new ItemConverter();
            var api = new ApiMock();
            var resultCreator = new ResultListCreator(validator, converter, api);
            var result = resultCreator.CreateResultList(api.GetTopStoryIds().Result, 2);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void CreateResultList_ApiThrows()
        {
            var validator = new ValidatorMock();
            var converter = new ItemConverter();
            var api = new ApiMock()
            {
                TopStoryIdsFunc = () => new List<int> { 1, 2 },
                GetItemFunc = id =>
                {
                    throw new AggregateException(new TaskCanceledException());
                }
            };
            var resultCreator = new ResultListCreator(validator, converter, api);
            var result = resultCreator.CreateResultList(api.GetTopStoryIds().Result, 2);
            Assert.IsNull(result);
        }

        [Test]
        public void CreateResultList_OneItem()
        {
            var validator = new ValidatorMock();
            var converter = new ItemConverter();
            var api = new ApiMock()
            {
                TopStoryIdsFunc = () => new List<int> { 1 },
                GetItemFunc = id => Creator.CreateRandomItem(id)
            };
            var resultCreator = new ResultListCreator(validator, converter, api);
            var result = resultCreator.CreateResultList(api.GetTopStoryIds().Result, 2);
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateResultList_FailedItem()
        {
            var validator = new ValidatorMock
            {
                ValidatorFunc = item => false
            };
            var converter = new ItemConverter();
            var api = new ApiMock()
            {
                TopStoryIdsFunc = () => new List<int> { 1 },
                GetItemFunc = id => Creator.CreateRandomItem(id)
            };
            var resultCreator = new ResultListCreator(validator, converter, api);
            var result = resultCreator.CreateResultList(api.GetTopStoryIds().Result, 2);
            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

    }
}
