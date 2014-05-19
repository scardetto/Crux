using System;
using FluentValidation;
using Microsoft.Practices.ServiceLocation;

namespace Crux.WebApi.Validation
{
    public class ValidatorLocator : IValidatorFactory
    {
        public IValidator<T> GetValidator<T>()
        {
            return GetValidatorFor(typeof(T)) as IValidator<T>;
        }

        public IValidator GetValidator(Type type)
        {
            if (type == null) return null;
            return GetValidatorFor(type) as IValidator;
        }

        private object GetValidatorFor(Type type)
        {
            return ServiceLocator.Current.GetInstance(MakeValidatorType(type));
        }

        private Type MakeValidatorType(Type type)
        {
            return typeof(AbstractValidator<>).MakeGenericType(type);
        }
    }
}
