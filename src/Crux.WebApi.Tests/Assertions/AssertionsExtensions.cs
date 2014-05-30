using System.Net.Http;

namespace Crux.WebApi.Testing.Assertions
{
    public static class AssertionsExtensions
    {
        public static HttpResponseMessageAssertions Should(this HttpResponseMessage response)
        {
            return new HttpResponseMessageAssertions(response);
        }
    }
}
