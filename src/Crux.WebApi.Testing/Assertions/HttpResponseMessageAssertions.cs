using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Crux.WebApi.Testing.Assertions
{
    public class HttpResponseMessageAssertions : ReferenceTypeAssertions<HttpResponseMessage, HttpResponseMessageAssertions>
    {
        public HttpResponseMessageAssertions(HttpResponseMessage response)
        {
            Subject = response;
        }

        public AndConstraint<HttpResponseMessageAssertions> BeOk(string because = "", params object[] reasonArgs)
        {
            return BeOfStatus(HttpStatusCode.OK, because, reasonArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> BeNotFound(string because = "", params object[] reasonArgs)
        {
            return BeOfStatus(HttpStatusCode.NotFound, because, reasonArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> BeBadRequest(string because = "", params object[] reasonArgs)
        {
            return BeOfStatus(HttpStatusCode.BadRequest, because, reasonArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> BeJson(string because = "", params object[] reasonArgs)
        {
            return BeOfMediaType(new MediaTypeHeaderValue("application/json"), because, reasonArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> BeText(string because = "", params object[] reasonArgs)
        {
            return BeOfMediaType(new MediaTypeHeaderValue("text/plain"), because, reasonArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> HaveStringContent(string expected, string because = "", params object[] reasonArgs)
        {
            var stringContent = Subject.Content.ReadAsStringAsync().Result;

            Execute.Assertion
                .ForCondition(stringContent.Equals(expected))
                .BecauseOf(because, reasonArgs)
                .FailWith("Expeceted response body '{0}', but found {1}.", expected, stringContent);

            return new AndConstraint<HttpResponseMessageAssertions>(this);
        }

        public AndConstraint<HttpResponseMessageAssertions> HaveContentStartWith(string expected, string because = "", params object[] reasonArgs)
        {
            var stringContent = Subject.Content.ReadAsStringAsync().Result;

            Execute.Assertion
                .ForCondition(stringContent.StartsWith(expected))
                .BecauseOf(because, reasonArgs)
                .FailWith("Expeceted response body to start with {0}, but found {1}.", expected, stringContent);

            return new AndConstraint<HttpResponseMessageAssertions>(this);
        }

        private AndConstraint<HttpResponseMessageAssertions> BeOfStatus(HttpStatusCode statusCode, string because = "", params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.StatusCode == statusCode)
                .BecauseOf(because, reasonArgs)
                .FailWith("Expeceted status code {0}, but found {1}.", statusCode, Subject.StatusCode);

            return new AndConstraint<HttpResponseMessageAssertions>(this);
        }

        public AndConstraint<HttpResponseMessageAssertions> BeOfMediaType(MediaTypeHeaderValue mediaType, string because = "", params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.Content.Headers.ContentType.MediaType.Equals(mediaType.MediaType))
                .BecauseOf(because, reasonArgs)
                .FailWith("Expeceted response media type {0}, but found {1}.", mediaType.MediaType, Subject.Content.Headers.ContentType.MediaType);

            return new AndConstraint<HttpResponseMessageAssertions>(this);
        }

        protected override string Context
        {
            get { return typeof (HttpResponseMessage).Name; }
        }
    }
}
