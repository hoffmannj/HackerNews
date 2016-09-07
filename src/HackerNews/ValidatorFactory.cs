using HackerNews.Implementations;
using HackerNews.Interfaces;
using HackerNews.Validators;

namespace HackerNews
{
    static class ValidatorFactory
    {
        /// <summary>
        /// Creates an ItemValidator with the defult set of validators
        /// </summary>
        /// <returns>Instance with the IItemValidator interface</returns>
        public static IItemValidator Create()
        {
            return new ItemValidator(new IValidator[] {
                new AuthorValidator(),
                new TitleValidator(),
                new PointsValidator(),
                new CommentsValidator(),
                new UriValidator()});
        }
    }
}
