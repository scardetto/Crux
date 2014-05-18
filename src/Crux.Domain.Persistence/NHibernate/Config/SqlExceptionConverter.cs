using System;
using System.Data.SqlClient;
using NHibernate;
using NHibernate.Exceptions;

namespace Crux.Domain.Persistence.NHibernate.Config
{
    public class SqlExceptionConverter : ISQLExceptionConverter
    {
        public Exception Convert(AdoExceptionContextInfo exceptionContext)
        {
            var sqlException = ADOExceptionHelper.ExtractDbException(exceptionContext.SqlException) as SqlException;

            if (sqlException != null) {
                switch (sqlException.Number) {
                    case 2627:
                    case 2601:
                    case 547:
                        return new ConstraintViolationException(
                            exceptionContext.SqlException.Message,
                            sqlException.InnerException,
                            exceptionContext.Sql,
                            null
                        );

                    case 208:
                        return new SQLGrammarException(
                            exceptionContext.SqlException.Message,
                            sqlException.InnerException,
                            exceptionContext.Sql
                        );

                    case 3960:
                        return new StaleObjectStateException(
                            exceptionContext.EntityName,
                            exceptionContext.EntityId
                        );
                }
            }

            return SQLStateConverter.HandledNonSpecificException(
                exceptionContext.SqlException,
                exceptionContext.Message,
                exceptionContext.Sql
            );
        }
    }
}
