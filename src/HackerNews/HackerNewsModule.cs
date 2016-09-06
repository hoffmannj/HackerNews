using HackerNews.Implementations;
using HackerNews.Interfaces;
using Ninject.Modules;

namespace HackerNews
{
    public class HackerNewsModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IApi>().To<HackerNewsApi>();
            this.Bind<IConverter>().To<ItemConverter>();
            this.Bind<IItemValidator>().ToMethod(context => ValidatorFactory.Create());
        }
    }
}
