using System;
using System.Collections.Generic;

namespace Crux.NancyFx.Infrastructure.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(IDictionary<string, string[]> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public IDictionary<string, string[]> ValidationErrors { get; private set; }
    }
}