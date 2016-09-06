using HackerNews.Validators;
using NUnit.Framework;

namespace HackerNewsTests
{
    [TestFixture]
    class Test_Validator_Uri
    {
        [Test]
        public void Validator_Uri_Good()
        {
            var item = Creator.CreateRandomItem(1);
            var validator = new UriValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Validator_Uri_Failed_Null()
        {
            var item = Creator.CreateRandomItem(1);
            item.Url = null;
            var validator = new UriValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validator_Uri_Failed_Wrong_1()
        {
            var item = Creator.CreateRandomItem(1);
            item.Url = "//drive/folder.file.txt";
            var validator = new UriValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validator_Uri_Failed_Wrong_2()
        {
            var item = Creator.CreateRandomItem(1);
            item.Url = "http:///a.com";
            var validator = new UriValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }
    }
}
