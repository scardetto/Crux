using System;
using Crux.NancyFx.Infrastructure.Exceptions;
using Nancy;
using Nancy.Responses;

namespace Crux.NancyFx.Infrastructure.Pipelines
{
    public class HttpBadRequestPipeline
    {
        public readonly static Func<NancyContext, Exception, Response> OnHttpBadRequest = (ctx, ex) =>
        {
            var badRequestException = ex as BadRequestException;
            if (badRequestException == null) return null;

            var jsonObject = new { errors = badRequestException.ValidationErrors };

            return new JsonResponse(jsonObject, new DefaultJsonSerializer()) {
                StatusCode = HttpStatusCode.BadRequest
            };
        };
    }
}
