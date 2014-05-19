using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Crux.WebApi.Exceptions;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Crux.WebApi.Validation
{
    public static class ValidationExtensions
    {
        public static void ThrowIfInvalid(this ValidationResult result)
        {
            if (result.IsValid) {
                return;
            }

            throw result.ToException();
        }

        public static HttpResponseException ToException(this ValidationResult result)
        {
            var validationErrors = result.Errors.Select(e => 
                new KeyValuePair<string, string>(e.PropertyName, e.ErrorMessage)
            );

            return HttpException.BadRequest(JsonConvert.SerializeObject(validationErrors));
        }
    }
}
