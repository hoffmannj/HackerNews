using HackerNews.Validators;
using NUnit.Framework;

namespace HackerNewsTests
{
    [TestFixture]
    class Test_Validator_Comments
    {
        [Test]
        public void Validator_Comments_Good()
        {
            var item = Creator.CreateRandomItem(1);
            var validator = new CommentsValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Validator_Comments_Failed_Low()
        {
            var item = Creator.CreateRandomItem(1);
            item.Descendants = -1;
            var validator = new CommentsValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validator_Comments_Failed_Null()
        {
            var item = Creator.CreateRandomItem(1);
            item.Descendants = null;
            var validator = new CommentsValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }
    }
}
