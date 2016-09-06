using HackerNews.Implementations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsTests
{
    [TestFixture]
    class Test_ItemConverter
    {
        [Test]
        public void ItemConverter_Good()
        {
            var converter = new ItemConverter();
            var item = Creator.CreateRandomItem(1);
            var result = converter.Convert(item, 1);

            Assert.NotNull(result);
            Assert.AreEqual(1, result.Rank);
        }

        [Test]
        public void ItemConverter_Failed_ScoreNull()
        {
            var converter = new ItemConverter();
            var item = Creator.CreateRandomItem(1);
            item.Score = null;
            Assert.Throws<NullReferenceException>(() =>
            {
                var result = converter.Convert(item, 1);
            });
        }

        [Test]
        public void ItemConverter_Failed_DescendantsNull()
        {
            var converter = new ItemConverter();
            var item = Creator.CreateRandomItem(1);
            item.Descendants = null;
            Assert.Throws<NullReferenceException>(() =>
            {
                var result = converter.Convert(item, 1);
            });
        }
    }
}
