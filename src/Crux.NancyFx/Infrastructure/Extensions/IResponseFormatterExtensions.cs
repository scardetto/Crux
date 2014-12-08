using System;
using FluentValidation.Results;
using Nancy;
using Nancy.Validation;

namespace Crux.NancyFx.Infrastructure.Extensions
{
    public static class IResponseFormatterExtensions
    {
        public static Response AsBadRequest(this IResponseFormatter response, ModelValidationResult validationResult)
        {
            if (validationResult.IsValid)
                throw new InvalidOperationException("The validation result contains no errors");

            return response.AsJson(new {errors = validationResult.GetFlattenedErrors()}, HttpStatusCode.BadRequest);
        }

        public static Response AsBadRequest(this IResponseFormatter response, params string[] baseErrors)
        {
            return response.AsJson(new {errors = new {baseErrors}}, HttpStatusCode.BadRequest);
        }

        public static Response AsBadRequest(this IResponseFormatter response, ValidationResult validationResult)
        {
            if (validationResult.IsValid)
                throw new InvalidOperationException("The validation result contains no errors");

            return response.AsJson(new { errors = validationResult.GetFlattenedErrors() }, HttpStatusCode.BadRequest);
        }
    }
}
