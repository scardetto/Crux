using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Exceptions;

namespace Crux.Domain.Persistence.NHibernate.Config
{
    /// <summary>
    /// This class allows you to customize how <see cref="T:System.Data.Common.DbException"/>s 
    /// thrown by the underlying data store are converted into NHibernate's 
    /// exception hierarchy.
    /// </summary>
    public class SqlExceptionMapper
    {
        private readonly IList<SqlExceptionMapping> _mappings;

        /// <summary>
        /// Creates a SqlExceptionMapper with default converters for SQL Server.
        /// </summary>
        public static SqlExceptionMapper Default()
        {
            var mappings = new List<SqlExceptionMapping> {
                SqlExceptionMapping.MatchOnSqlErrorCode(
                    new[] {2627, 2601, 547},
                    c => new ConstraintViolationException(c.SqlException.Message, c.SqlException.InnerException, c.Sql, null)
                ),
                SqlExceptionMapping.MatchOnSqlErrorCode(
                    new[] {208},
                    c => new SQLGrammarException(c.SqlException.Message, c.SqlException.InnerException, c.Sql)
                ),
                SqlExceptionMapping.MatchOnSqlErrorCode(
                    new[] {3960},
                    c => new StaleObjectStateException(c.EntityName, c.EntityId)
                )
            };

            return new SqlExceptionMapper(mappings);
        }

        /// <summary>
        /// Creates an empty SqlExceptionMapper with no mappings.
        /// </summary>
        public SqlExceptionMapper() : this(new List<SqlExceptionMapping>()) { }

        /// <summary>
        /// Creates a SqlExceptionMapper with the provided mappings.
        /// </summary>
        /// <param name="mappings">An <see cref="IList{T}"/> of mappings 
        ///     containing the conversion rules.</param>
        public SqlExceptionMapper(IList<SqlExceptionMapping> mappings)
        {
            _mappings = mappings;
        }

        /// <summary>
        /// Iterates through the provided mappings looking for a matching rule. 
        /// If found, the converter function for that mapping is called.
        /// </summary>
        /// <param name="exceptionContext">Available info about the exception 
        ///     being thrown.</param>
        public Exception Convert(AdoExceptionContextInfo exceptionContext)
        {
            var mapping = _mappings.FirstOrDefault(m => m.Matcher.Invoke(exceptionContext));

            return mapping != null
                ? mapping.Converter.Invoke(exceptionContext)
                : SQLStateConverter.HandledNonSpecificException(exceptionContext.SqlException, exceptionContext.Message, exceptionContext.Sql);
        }

        /// <summary>
        /// Adds a mapping rule to the top of the list.
        /// </summary>
        public void AddExceptionMappingToTop(SqlExceptionMapping mapping)
        {
            _mappings.Insert(0, mapping);
        }

        /// <summary>
        /// Adds a mapping rule to the end of the list.
        /// </summary>
        public void AddExceptionMappingToEnd(SqlExceptionMapping mapping)
        {
            _mappings.Add(mapping);
        }

        /// <summary>
        /// Clears the internal mappings.
        /// </summary>
        public void ClearMappings()
        {
            _mappings.Clear();
        }
    }
}
