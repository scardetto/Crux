using Crux.Domain.UoW;
using NHibernate;

namespace Crux.Domain.Persistence.NHibernate
{
    public class NHibernateUnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        private readonly INHibernateUnitOfWork _unitOfWork;
        private readonly UnitOfWorkTransactionOptions _options;
        private ITransaction _transaction;

        public NHibernateUnitOfWorkTransaction(INHibernateUnitOfWork unitOfWork, UnitOfWorkTransactionOptions options)
        {
            _unitOfWork = unitOfWork;
            _options = options;
        }

        public void Begin()
        {
            _transaction = _unitOfWork.CurrentSession.BeginTransaction(_options.IsolationLevel);
        }

        public void Commit()
        {
            if (!_transaction.WasCommitted) {
                _transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (!_transaction.WasRolledBack) {
                _transaction.Rollback();
            }
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        public bool WasRolledBack
        {
            get { return _transaction.WasRolledBack; }
        }
    }
}
