using System;

namespace HackerNews.Helpers
{
    class ErrorMessageHelper
    {
        public static void WriteErrorMessages(string mainMessage, Exception ex)
        {
            Console.Error.WriteLine(mainMessage);
            var inner = GetDeepestException(ex);
            if (IsTimeout(inner)) Console.Error.WriteLine("Error: query timed out.");
            else Console.Error.WriteLine("Error: {0}", inner.Message);
        }

        private static Exception GetDeepestException(Exception ex)
        {
            Exception inner = ex;
            while (inner.InnerException != null) inner = inner.InnerException;
            return inner;
        }

        private static bool IsTimeout(Exception ex)
        {
            return ex.GetType() == typeof(System.Threading.Tasks.TaskCanceledException);
        }
    }
}
