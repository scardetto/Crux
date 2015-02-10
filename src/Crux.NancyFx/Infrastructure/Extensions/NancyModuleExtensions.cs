using System;
using Crux.NancyFx.Infrastructure.Exceptions;
using Nancy;
using Nancy.Validation;

namespace Crux.NancyFx.Infrastructure.Extensions
{
    public static class NancyModuleExtensions
    {
        public static bool TryGetQueryParam<T>(this NancyModule module, string parameterName, out T value)
        {
            value = default(T);

            try {
                value = (T)module.Request.Query[parameterName];
            }
            catch (Exception) {
            }

            return value != null && !value.Equals(default(T));
        }

        public static void ValidateModel<T>(this NancyModule module, T input) where T : class
        {
            var validation = module.Validate(input);
            if (!validation.IsValid) {
                throw new BadRequestException(validation);
            }
        }
    }
}
