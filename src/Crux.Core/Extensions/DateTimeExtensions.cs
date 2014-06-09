using System;

namespace Crux.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToTheDay(this DateTime date)
        {
            return date.RoundTicks(TimeSpan.TicksPerDay);
        }

        public static DateTime ToTheHour(this DateTime date)
        {
            return date.RoundTicks(TimeSpan.TicksPerHour);
        }

        public static DateTime ToTheMinute(this DateTime date)
        {
            return date.RoundTicks(TimeSpan.TicksPerMinute);
        }

        public static DateTime ToTheSecond(this DateTime date)
        {
            return date.RoundTicks(TimeSpan.TicksPerSecond);
        }

        public static DateTime ToTheMillisecond(this DateTime date)
        {
            return date.RoundTicks(TimeSpan.TicksPerMillisecond);
        }

        public static DateTime? ToTheDay(this DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            return date.Value.ToTheDay();
        }

        public static DateTime? ToTheHour(this DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            return date.Value.ToTheHour();
        }

        public static DateTime? ToTheMinute(this DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            return date.Value.ToTheMinute();
        }

        public static DateTime? ToTheSecond(this DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            return date.Value.ToTheSecond();
        }

        public static DateTime? ToTheMillisecond(this DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            return date.Value.ToTheMillisecond();
        }

        public static DateTime RoundTicks(this DateTime date, long interval)
        {
            return new DateTime(date.Ticks - (date.Ticks % interval), date.Kind);
        }

        public static bool IsInTheFuture(this DateTime date)
        {
            return date > DateTime.Now;
        }

        public static bool IsInThePast(this DateTime date)
        {
            return date < DateTime.Now;
        }

        public static bool IsNowOrInTheFuture(this DateTime date)
        {
            return date >= DateTime.Now;
        }

        public static bool IsNowOrInThePast(this DateTime date)
        {
            return date <= DateTime.Now;
        }
    }
}
