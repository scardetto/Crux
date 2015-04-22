using System;
using System.Collections.Generic;
using Crux.NancyFx.Infrastructure.Exceptions;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Newtonsoft.Json;

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

        public static T BindAndValidateModel<T>(this NancyModule module) where T : class
        {
            return module.BindAndValidateModel<T>(null);
        }

        public static T BindAndValidateModel<T>(this NancyModule module, Action<T> hookToSetModel) where T : class
        {
            try {
                var input = module.Bind<T>();
                
                if (hookToSetModel != null) {
                    hookToSetModel(input);
                }
                
                module.ValidateModel(input);

                return input;
            }
            catch (JsonReaderException ex) {
                var errors = new Dictionary<string, string[]> { { ex.Path, new[] { ex.Message } } };
                throw new BadRequestException(errors);
            }
        }

        public static void ValidateModel<T>(this NancyModule module, T input) where T : class
        {
            var validation = module.Validate(input);
            if (validation.IsValid) return;
            
            throw new BadRequestException(validation.GetFlattenedErrors());
        }
    }
}
