using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FluentValidation;
using Microsoft.Practices.ServiceLocation;

namespace Crux.WebApi.Validation
{
    public sealed class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            WhenTheRequestCanBeValidated(actionContext, 
                (validator, argument) => validator.Validate(argument).ThrowIfInvalid()
            );
;       }

        private void WhenTheRequestCanBeValidated(HttpActionContext actionContext, Action<IValidator, object> action)
        {
            var argument = GetTheActionArgument(actionContext);

            if (argument == null) {
                return;
            }

            var validator = GetTheValidatorForTheArgumentType(argument);

            if (validator == null) {
                return;
            }

            action(validator, argument);
        }

        private static IValidator GetTheValidatorForTheArgumentType(object actionArgument)
        {
            return ServiceLocator.Current
                .GetInstance<IValidatorFactory>()
                .GetValidator(actionArgument.GetType());
        }

        private static object GetTheActionArgument(HttpActionContext actionContext)
        {
            var actionArguments = actionContext.ActionArguments;

            return actionArguments.Count() == 1 
                ? actionArguments.Single().Value 
                : null;
        }
    }
}
