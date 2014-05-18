using System;
using System.Data;
using Crux.Core.Extensions;

namespace Crux.Domain.UoW
{
    public class UnitOfWorkTransactionOptions
    {
        public IsolationLevel IsolationLevel { get; set; }
        public TimeSpan Timeout { get; set; }

        public static UnitOfWorkTransactionOptions Default()
        {
            return new UnitOfWorkTransactionOptions {
                IsolationLevel = IsolationLevel.Serializable,
            };
        }

        public static UnitOfWorkTransactionOptions DirtyReads()
        {
            return WithIsolationLevel(IsolationLevel.ReadUncommitted);
        }

        public static UnitOfWorkTransactionOptions LongOperation()
        {
            return WithTimeout(10.Minutes());
        }

        public static UnitOfWorkTransactionOptions WithIsolationLevel(IsolationLevel level)
        {
            var options = Default();
            options.IsolationLevel = level;

            return options;
        }

        public static UnitOfWorkTransactionOptions WithTimeout(TimeSpan timeSpan)
        {
            var options = Default();
            options.Timeout = timeSpan;

            return options;
        }
    }
}
