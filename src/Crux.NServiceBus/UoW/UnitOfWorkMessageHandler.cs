using System;
using Crux.Domain.UoW;
using NServiceBus;
using NServiceBus.Logging;

namespace Crux.NServiceBus.UoW
{
    public abstract class UnitOfWorkMessageHandler<T> : IHandleMessages<T>
    {
        private readonly UnitOfWorkTransactionOptions _options;
        private readonly ILog _log;

        private IUnitOfWork _unitOfWork;

        protected UnitOfWorkMessageHandler()
            : this(UnitOfWorkTransactionOptions.Default()) { }

        protected UnitOfWorkMessageHandler(UnitOfWorkTransactionOptions options)
        {
            _options = options;
            _log = LogManager.GetLogger(GetType());
        }

        public IUnitOfWork UnitOfWork
        {
            set { _unitOfWork = value; }
        }

        protected ILog Log
        {
            get { return _log; }
        }

        public void Handle(T message)
        {
            using (var scope = _unitOfWork.CreateTransactionalScope(_options)) {
                try {
                    HandleMessage(message);

                    scope.Complete();
                } catch (Exception e) {
                    _log.Error("Unhandled exception", e);
                    throw;
                }
            }
        }

        public abstract void HandleMessage(T message);
    }
}
