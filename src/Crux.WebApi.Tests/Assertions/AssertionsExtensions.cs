using System.Net.Http;

namespace Crux.WebApi.Tests.Assertions
{
    public static class AssertionsExtensions
    {
        public static HttpResponseMessageAssertions Should(this HttpResponseMessage response)
        {
            return new HttpResponseMessageAssertions(response);
        }
    }
}
