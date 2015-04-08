using System;
using Crux.Domain.Persistence.NHibernate;
using Crux.Domain.UoW;
using Crux.NancyFx.Infrastructure.Extensions;
using Nancy;
using NHibernate.Context;
using StructureMap;

namespace Crux.NancyFx.Infrastructure.Pipelines
{
    public static class DbTransactionPipeline
    {
        private const string REQUEST_TRANSACTIONAL_SCOPE_KEY = "transactional_scope";

        public static Func<NancyContext, Response> BeforeEveryRequest(IContainer requestContainer)
        {
            return ctx =>
            {
                if (ctx.IsHttpOptions()) return null;

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
            if (ctx.IsHttpOptions()) return;

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
