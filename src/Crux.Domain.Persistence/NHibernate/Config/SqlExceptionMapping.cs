using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using NHibernate.Exceptions;

namespace Crux.Domain.Persistence.NHibernate.Config
{
    /// <summary>
    /// Represents a single matching and conversion rule for the <seealso cref="SqlExceptionMapper"/>.
    /// </summary>
    public class SqlExceptionMapping
    {
        /// <summary>
        /// Returns a function that takes an AdoExceptionContextInfo and 
        /// determines if this mapping should be used to convert the exception.
        /// </summary>
        public Func<AdoExceptionContextInfo, bool> Matcher { get; private set; }

        /// <summary>
        /// Returns a function that takes an AdoExceptionContextInfo and 
        /// converts it to a new exception.
        /// </summary>
        public Func<AdoExceptionContextInfo, Exception> Converter { get; private set; }

        private SqlExceptionMapping() { }

        /// <summary>
        /// Creates a SqlExceptionMapping that will match if the error code in 
        /// the SqlException matches any of the error codes passed to this function.
        /// </summary>
        /// <param name="errorCodes">The error codes on the SqlException that 
        /// will cause the mapping to match.</param>
        /// <param name="converter">The function that will be used to convert the exception.</param>
        public static SqlExceptionMapping MatchOnSqlErrorCode(IEnumerable<int> errorCodes, Func<AdoExceptionContextInfo, Exception> converter)
        {
            return new SqlExceptionMapping {
                Matcher = c => {
                    var sqlException = ADOExceptionHelper.ExtractDbException(c.SqlException) as SqlException;
                    return sqlException != null && errorCodes.Contains(sqlException.ErrorCode);
                },
                Converter = converter
            };
        }
    }
}