using HackerNews.Implementations;
using HackerNews.Interfaces;
using HackerNews.Validators;

namespace HackerNews
{
    static class ValidatorFactory
    {
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
