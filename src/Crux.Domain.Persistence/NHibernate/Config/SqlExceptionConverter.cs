using System;
using NHibernate.Exceptions;

namespace Crux.Domain.Persistence.NHibernate.Config
{
    public class SqlExceptionConverter : ISQLExceptionConverter
    {
        private readonly SqlExceptionMapper _mapper;

        public SqlExceptionConverter()
        {
            _mapper = SqlExceptionMapper.Default();
        }

        public Exception Convert(AdoExceptionContextInfo exceptionContext)
        {
            return _mapper.Convert(exceptionContext);
        }
    }
}
