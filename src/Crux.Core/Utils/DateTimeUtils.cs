using System;

namespace Crux.Core.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime GetFirstDateOfCurrentYear()
        {
            return new DateTime(DateTime.Now.Year, 1, 1);
        }

        public static DateTime GetSqlServerMaxSmallDateTime()
        {
            return new DateTime(2079, 06, 06, 23, 59, 00);
        }
    }
}
