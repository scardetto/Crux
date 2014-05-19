using System.Web.Http.Filters;

namespace Crux.WebApi.Extensions
{
    public static class HttpActionExecutedContextExtensions
    {
        public static bool HasException(this HttpActionExecutedContext context)
        {
            return !context.HasNoException();
        }

        public static bool HasNoException(this HttpActionExecutedContext context)
        {
            return context.Exception == null;
        }
    }
}
