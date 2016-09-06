using HackerNews.Validators;
using NUnit.Framework;

namespace HackerNewsTests
{
    class Test_Validator_Title
    {
        [Test]
        public void Validator_Title_Good()
        {
            var item = Creator.CreateRandomItem(1);
            item.Title = new string('a', 256);
            var validator = new TitleValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Validator_Title_Failed_Null()
        {
            var item = Creator.CreateRandomItem(1);
            item.Title = null;
            var validator = new TitleValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validator_Title_Failed_Empty()
        {
            var item = Creator.CreateRandomItem(1);
            item.Title = "";
            var validator = new TitleValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validator_Title_Failed_TooLong()
        {
            var item = Creator.CreateRandomItem(1);
            item.Title = new string('a', 257);
            var validator = new TitleValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }
    }
}
