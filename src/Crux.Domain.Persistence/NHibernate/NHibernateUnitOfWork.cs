using System;
using Crux.Domain.UoW;
using NHibernate;

namespace Crux.Domain.Persistence.NHibernate
{
    public interface INHibernateUnitOfWork : IUnitOfWork
    {
        ISession CurrentSession { get; }
    }

    public class NHibernateUnitOfWork : INHibernateUnitOfWork
    {
        private readonly ISessionFactory _factory;

        public ISession CurrentSession { get; private set; }

        public IUnitOfWorkScope CreateScope()
        {
            return new UnitOfWorkScope(this);
        }

        public ITransactionalUnitOfWorkScope CreateTransactionalScope()
        {
            return CreateTransactionalScope(UnitOfWorkTransactionOptions.Default());
        }

        public ITransactionalUnitOfWorkScope CreateTransactionalScope(UnitOfWorkTransactionOptions options)
        {
            return new TransactionalUnitOfWorkScope(this, new NHibernateUnitOfWorkTransaction(this, options));
        }

        public NHibernateUnitOfWork(ISessionFactory factory)
        {
            _factory = factory;
        }

        public void Start()
        {
            AssertInValidState();

            CurrentSession = _factory.OpenSession();
        }

        private void AssertInValidState()
        {
            if (CurrentSession != null) {
                Dispose();
                throw new ObjectDisposedException("Previous session was not properly disposed.");
            }
        }

        public void Flush()
        {
            CurrentSession.Flush();
        }

        public void Clear()
        {
            CurrentSession.Clear();
        }

        public void Dispose()
        {
            if (CurrentSession == null) {
                return;
            }

            CurrentSession.Dispose();
            CurrentSession = null;
        }
    }
}
