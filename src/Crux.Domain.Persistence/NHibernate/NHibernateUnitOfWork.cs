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
        private readonly IDbConnectionProvider _connectionProvider;

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

        public NHibernateUnitOfWork(ISessionFactory factory, IDbConnectionProvider connectionProvider)
        {
            _factory = factory;
            _connectionProvider = connectionProvider;
        }

        public void Start()
        {
            AssertInValidState();

            CurrentSession = _factory.OpenSession(_connectionProvider.GetConnection());
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

            //must close connection manually since it was manually opened and given to NH
            CurrentSession.Connection.Close(); 
            CurrentSession.Dispose();
            CurrentSession = null;
        }
    }
}
