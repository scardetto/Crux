using System;
using Nancy.Validation;

namespace Crux.NancyFx.Infrastructure.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(ModelValidationResult modelValidationResult)
        {
            ModelModelValidationResult = modelValidationResult;
        }

        public ModelValidationResult ModelModelValidationResult { get; private set; }
    }
}