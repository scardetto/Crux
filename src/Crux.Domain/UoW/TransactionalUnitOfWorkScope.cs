namespace Crux.Domain.UoW
{
    public interface ITransactionalUnitOfWorkScope : IUnitOfWorkScope
    {
        bool WasRolledBack { get; }
    }

    public class TransactionalUnitOfWorkScope : ITransactionalUnitOfWorkScope
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkTransaction _transaction;

        public TransactionalUnitOfWorkScope(
            IUnitOfWork unitOfWork,
            IUnitOfWorkTransaction transaction)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.Start();
            _transaction = transaction;
        }

        public bool WasRolledBack
        {
            get { return _transaction.WasRolledBack; }
        }

        public void Complete()
        {
            _unitOfWork.Flush();
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _unitOfWork.Dispose();
        }
    }

}