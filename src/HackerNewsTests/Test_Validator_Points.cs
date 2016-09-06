using HackerNews.Validators;
using NUnit.Framework;

namespace HackerNewsTests
{
    [TestFixture]
    class Test_Validator_Points
    {
        [Test]
        public void Validator_Points_Good()
        {
            var item = Creator.CreateRandomItem(1);
            var validator = new PointsValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Validator_Points_Failed_Low()
        {
            var item = Creator.CreateRandomItem(1);
            item.Score = -1;
            var validator = new PointsValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validator_Points_Failed_Null()
        {
            var item = Creator.CreateRandomItem(1);
            item.Score = null;
            var validator = new PointsValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }
    }
}
