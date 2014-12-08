using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Nancy.Validation;

namespace Crux.NancyFx.Infrastructure.Extensions
{
    public static class ModelValidationResultExtensions
    {
        public static IDictionary<string, string[]> GetFlattenedErrors(this ModelValidationResult validationResult)
        {
            return validationResult.Errors.ToDictionary(errorItem => errorItem.Key, errorItem => errorItem.Value.Select(error => error.ErrorMessage).ToArray());
        }

        public static IDictionary<string, string[]> GetFlattenedErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors.ToDictionary(errorItem => errorItem.PropertyName, errorItem => new [] {errorItem.ErrorMessage});
        }
    }
}
