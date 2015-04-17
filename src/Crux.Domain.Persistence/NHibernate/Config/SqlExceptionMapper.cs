using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using NHibernate;
using NHibernate.Exceptions;

namespace Crux.Domain.Persistence.NHibernate.Config
{
    public class SqlExceptionMapper
    {
        private readonly IList<SqlExceptionMapping> _mappings;

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

        public SqlExceptionMapper(IList<SqlExceptionMapping> mappings)
        {
            _mappings = mappings;
        }

        public Exception Convert(AdoExceptionContextInfo exceptionContext)
        {
            var mapping = _mappings.FirstOrDefault(m => m.Matcher.Invoke(exceptionContext));

            return mapping != null
                ? mapping.Converter.Invoke(exceptionContext)
                : SQLStateConverter.HandledNonSpecificException(exceptionContext.SqlException, exceptionContext.Message, exceptionContext.Sql);
        }

        public void AddExceptionMappingToTop(SqlExceptionMapping mapping)
        {
            _mappings.Insert(0, mapping);
        }

        public void AddExceptionMappingToEnd(SqlExceptionMapping mapping)
        {
            _mappings.Add(mapping);
        }

        public void ClearMappings()
        {
            _mappings.Clear();
        }
    }

    public class SqlExceptionMapping
    {
        public Func<AdoExceptionContextInfo, bool> Matcher { get; private set; }
        public Func<AdoExceptionContextInfo, Exception> Converter { get; private set; }

        private SqlExceptionMapping() { }

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
