using System;

namespace HackerNews.Helpers
{
    class AssertHelper
    {
        public static void AssertArgumentNotNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void AssertFieldNotNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new NullReferenceException(name);
            }
        }
    }
}
