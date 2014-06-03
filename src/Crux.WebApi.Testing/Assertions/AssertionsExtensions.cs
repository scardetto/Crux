using System.Net.Http;
using System.Runtime.CompilerServices;
using FluentAssertions;

namespace Crux.WebApi.Testing.Assertions
{
    public static class AssertionsExtensions
    {
        static AssertionsExtensions()
        {
            RuntimeHelpers.RunClassConstructor(typeof (AssertionExtensions).TypeHandle);
        }

        public static HttpResponseMessageAssertions Should(this HttpResponseMessage response)
        {
            return new HttpResponseMessageAssertions(response);
        }
    }
}
