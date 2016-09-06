using Fclp;
using HackerNews.Helpers;
using HackerNews.Interfaces;
using HackerNews.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HackerNews
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger(typeof(Program));
        private const int MINPOSTSCOUNT = 1;
        private const int MAXPOSTSCOUNT = 100;

        private static int postsCount = -1;

        static void Main(string[] args)
        {
            if (!HandleArguments(args)) return;

            var kernel = InitializeDI();

            var api = kernel.Get<IApi>();
            var resultCreator = kernel.Get<ResultListCreator>();

            var topStoryIds = GetTopStoryIds(api);
            if (topStoryIds == null) return;

            var result = GetOutputList(resultCreator, topStoryIds);
            if (result == null) return;

            Console.Out.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));

            NLog.LogManager.Configuration = null;
        }

        private static bool HandleArguments(string[] args)
        {
            var p = new FluentCommandLineParser();

            p.Setup<int>("posts")
               .Callback(posts => postsCount = posts)
               .Required();

            p.Parse(args);

            if (postsCount < MINPOSTSCOUNT || postsCount > MAXPOSTSCOUNT)
            {
                Console.Out.WriteLine("Usage: hackernews --posts <n>");
                Console.Out.WriteLine("Where: 1 <= n <= 100");
                return false;
            }
            return true;
        }

        private static IKernel InitializeDI()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }

        private static List<int> GetTopStoryIds(IApi api)
        {
            List<int> topStories = null;

            try
            {
                topStories = api.GetTopStoryIds().Result;
            }
            catch (Exception ex)
            {
                ErrorMessageHelper.WriteErrorMessages("Couldn't get the list of the top posts", ex);
            }

            return topStories;
        }

        private static List<OutputItem> GetOutputList(ResultListCreator resultCreator, List<int> ids)
        {
            List<OutputItem> result = null;

            try
            {
                result = resultCreator.CreateResultList(ids, postsCount);
            }
            catch (Exception ex)
            {
                ErrorMessageHelper.WriteErrorMessages("Couldn't create the result list", ex);
            }

            return result;
        }
    }
}
