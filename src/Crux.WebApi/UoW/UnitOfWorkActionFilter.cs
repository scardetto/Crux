using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Crux.Domain.Persistence.NHibernate;
using Crux.Domain.UoW;
using Crux.WebApi.Extensions;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Context;

namespace Crux.WebApi.UoW
{
    public sealed class UnitOfWorkActionFilter : ActionFilterAttribute
    {
        private readonly RequestScopeAccessor<ITransactionalUnitOfWorkScope> _scopeAccessor;

        public UnitOfWorkActionFilter()
        {
            _scopeAccessor = new RequestScopeAccessor<ITransactionalUnitOfWorkScope>("TransactionalScope");
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var unitOfWork = ServiceLocator.Current.GetInstance<INHibernateUnitOfWork>();
            _scopeAccessor.Set(unitOfWork.CreateTransactionalScope());

            var session = unitOfWork.CurrentSession;
            CurrentSessionContext.Bind(session);
            session.BeginTransaction();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var scope = _scopeAccessor.Get();

            try {
                if (ScopeCanComplete(scope, actionExecutedContext)) {
                    scope.Complete();
                }
            } finally {
                scope.Dispose();
            }
        }

        private bool ScopeCanComplete(ITransactionalUnitOfWorkScope scope, HttpActionExecutedContext context)
        {
            return (context.HasNoException() && !scope.WasRolledBack);
        }
    }
}
