using Nancy;

namespace Crux.NancyFx.Infrastructure.Extensions
{
    public static class NancyContextExtensions
    {
        public static bool IsHttpOptions(this NancyContext context)
        {
            return context.Request.Method.Equals("OPTIONS");
        }
    }
}
