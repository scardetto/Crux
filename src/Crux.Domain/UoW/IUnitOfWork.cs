using System;

namespace Crux.Domain.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        void Start();

        void Flush();

        void Clear();

        /// <summary>
        /// Creates a non transactional scope for an uninitialized IUnitOfWork
        /// </summary>
        /// <returns></returns>
        IUnitOfWorkScope CreateScope();

        /// <summary>
        /// Creates a transactional scope for an uninitialized IUnitOfWork
        /// </summary>
        /// <returns></returns>
        ITransactionalUnitOfWorkScope CreateTransactionalScope();

        /// <summary>
        /// Creates a transactional scope for an uninitialized IUnitOfWork
        /// </summary>
        /// <returns></returns>
        ITransactionalUnitOfWorkScope CreateTransactionalScope(UnitOfWorkTransactionOptions options);
    }
}