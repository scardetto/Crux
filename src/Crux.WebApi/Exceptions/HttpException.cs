using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Crux.WebApi.Exceptions
{
    public static class HttpException
    {
        public static HttpResponseException BadRequest(string message = "")
        {
            return ToException(ToMessage(HttpStatusCode.BadRequest, message));
        }

        public static HttpResponseException NotFound(string message = "")
        {
            return ToException(ToMessage(HttpStatusCode.NotFound, message));
        }

        public static HttpResponseException Unauthorized(string message = "")
        {
            return ToException(ToMessage(HttpStatusCode.Unauthorized, message));
        }

        private static HttpResponseMessage ToMessage(HttpStatusCode status, String message = "")
        {
            return new HttpResponseMessage(status) {
                Content = new StringContent(message)
            };
        }

        private static HttpResponseException ToException(HttpResponseMessage message)
        {
            return new HttpResponseException(message);
        }
    }
}
