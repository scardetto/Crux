using System;
using NHibernate.Exceptions;

namespace Crux.Domain.Persistence.NHibernate.Config
{
    /// <summary>
    /// Delegates calls from the NHibernate exception pipeline to a SqlExceptionMapper.
    /// </summary>
    public class SqlExceptionConverter : ISQLExceptionConverter
    {
        private readonly SqlExceptionMapper _mapper;

        /// <summary>
        /// Creates a new SqlExceptionConverter with default mapping rules.
        /// </summary>
        public SqlExceptionConverter() : this(SqlExceptionMapper.Default()) { }

        /// <summary>
        /// Creates a new SqlExceptionConverter with the provided mapping rules.
        /// </summary>
        public SqlExceptionConverter(SqlExceptionMapper mapping)
        {
            _mapper = mapping;
        }

        /// <summary>
        /// Called by NHibernate when an exception occurs. Uses the 
        /// SqlExceptionMapper to determine if and how the exception is 
        /// converted.
        /// </summary>
        /// <param name="exceptionContext">Available info about the exception being thrown.</param>
        public Exception Convert(AdoExceptionContextInfo exceptionContext)
        {
            return _mapper.Convert(exceptionContext);
        }
    }
}
