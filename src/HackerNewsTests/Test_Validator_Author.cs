using HackerNews.Validators;
using NUnit.Framework;

namespace HackerNewsTests
{
    class Test_Validator_Author
    {
        [Test]
        public void Validator_Author_Good()
        {
            var item = Creator.CreateRandomItem(1);
            item.By = new string('a', 256);
            var validator = new AuthorValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Validator_Author_Failed_Null()
        {
            var item = Creator.CreateRandomItem(1);
            item.By = null;
            var validator = new AuthorValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validator_Author_Failed_Empty()
        {
            var item = Creator.CreateRandomItem(1);
            item.By = "";
            var validator = new AuthorValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validator_Author_Failed_TooLong()
        {
            var item = Creator.CreateRandomItem(1);
            item.By = new string('a', 257);
            var validator = new AuthorValidator();
            var result = validator.Validate(item);
            Assert.AreEqual(false, result);
        }
    }
}
