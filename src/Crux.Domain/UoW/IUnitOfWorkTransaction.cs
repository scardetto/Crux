using System;

namespace Crux.Domain.UoW
{
    public interface IUnitOfWorkTransaction : IDisposable
    {
        void Begin();

        void Commit();

        void Rollback();

        bool WasRolledBack { get; }
    }
}
