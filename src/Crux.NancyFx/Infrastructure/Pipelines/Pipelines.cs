using System;
using Crux.Domain.Persistence.NHibernate;
using Crux.Domain.UoW;
using Crux.NancyFx.Infrastructure.Exceptions;
using Crux.NancyFx.Infrastructure.Extensions;
using Nancy;
using Nancy.Responses;
using NHibernate.Context;
using StructureMap;

namespace Crux.NancyFx.Infrastructure.Pipelines
{
    public static class Pipelines
    {
        private const string REQUEST_TRANSACTIONAL_SCOPE_KEY = "transactional_scope";

        public readonly static Func<NancyContext, Exception, Response> OnError = (ctx, ex) =>
        {
            var badRequestException = ex as BadRequestException;
            if (badRequestException == null) return ctx.Response;

            var validationResult = badRequestException.ModelModelValidationResult;
            var flattenedErrors = new { errors = validationResult.GetFlattenedErrors() };

            return new JsonResponse(flattenedErrors, new DefaultJsonSerializer()) {
                StatusCode = HttpStatusCode.BadRequest
            };
        };

        public static Func<NancyContext, Response> BeforeEveryRequest(IContainer requestContainer)
        {
            return ctx => {
                var unitOfWork = requestContainer.GetInstance<INHibernateUnitOfWork>();
                var scope = unitOfWork.CreateTransactionalScope(UnitOfWorkTransactionOptions.DirtyReads());
                var session = unitOfWork.CurrentSession;

                ctx.Items.Add(REQUEST_TRANSACTIONAL_SCOPE_KEY, scope);

                CurrentSessionContext.Bind(session);

                session.BeginTransaction();

                return ctx.Response;
            };
        }

        public static Action<NancyContext> AfterEveryRequest = ctx =>
        {
            if (ctx.Items.ContainsKey(REQUEST_TRANSACTIONAL_SCOPE_KEY)) {
                var scope = (ITransactionalUnitOfWorkScope)ctx.Items[REQUEST_TRANSACTIONAL_SCOPE_KEY];

                try {
                    if (ScopeCanComplete(scope)) {
                        scope.Complete();
                    }
                }
                finally {
                    scope.Dispose();
                }
            }
        };

        private static bool ScopeCanComplete(ITransactionalUnitOfWorkScope scope)
        {
            return !scope.WasRolledBack;
        }
    }
}
