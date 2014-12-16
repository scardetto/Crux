using System;
using System.Collections.Generic;
using System.Linq;
using Crux.NancyFx.Infrastructure.Exceptions;
using Nancy;
using Nancy.Validation;

namespace Crux.NancyFx.Infrastructure.Extensions
{
    public static class NancyModuleExtensions
    {
        public static IEnumerable<object> EnumToIdValuePair<T>(this NancyModule module) where T : struct
        {
            return ((T[]) Enum.GetValues(typeof (T)))
                .Select((x, i) => new {id = i, value = x.ToString()});
        }

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
