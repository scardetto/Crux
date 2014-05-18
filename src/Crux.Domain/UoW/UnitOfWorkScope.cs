using System;

namespace Crux.Domain.UoW
{
    public interface IUnitOfWorkScope : IDisposable
    {
        void Complete();
    }

    public class UnitOfWorkScope : IUnitOfWorkScope
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkScope(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.Start();
        }

        public void Complete()
        {
            _unitOfWork.Flush();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}