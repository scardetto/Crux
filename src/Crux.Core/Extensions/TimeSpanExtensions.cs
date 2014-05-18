using System;

namespace Crux.Core.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan Ticks(this long interval)
        {
            return new TimeSpan(interval);
        }

        public static TimeSpan Milliseconds(this int interval)
        {
            return new TimeSpan(0, 0, 0, 0, interval);
        }

        public static TimeSpan Seconds(this int interval)
        {
            return new TimeSpan(0, 0, 0, interval);
        }

        public static TimeSpan Minutes(this int interval)
        {
            return new TimeSpan(0, 0, interval, 0);
        }

        public static TimeSpan Hours(this int interval)
        {
            return new TimeSpan(0, interval, 0, 0);
        }

        public static TimeSpan Days(this int interval)
        {
            return new TimeSpan(interval, 0, 0, 0);
        }

        public static TimeSpan Weeks(this int interval)
        {
            return new TimeSpan(interval * 7, 0, 0, 0);
        }

        public static TimeSpan Months(this int interval)
        {
            return new MonthSpan(interval);
        }

        public static TimeSpan Quarters(this int interval)
        {
            return new QuarterSpan(interval);
        }

        public static TimeSpan Years(this int interval)
        {
            return new YearSpan(interval);
        }

        public static DateTime FromNow(this TimeSpan span)
        {
            return From(span, DateTime.Now);
        }

        public static DateTime FromToday(this TimeSpan span)
        {
            return From(span, DateTime.Today);
        }

        public static DateTime From(this TimeSpan span, DateTime startDate)
        {
            return startDate.Add(span);
        }

        public static DateTime Ago(this TimeSpan span)
        {
            return Since(span, DateTime.Now);
        }

        public static DateTime Since(this TimeSpan span, DateTime startDate)
        {
            return startDate.Add(-span);
        }

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
            if (!date.HasValue) {
                return null;
            }

            return date.Value.ToTheDay();
        }

        public static DateTime? ToTheHour(this DateTime? date)
        {
            if (!date.HasValue) {
                return null;
            }

            return date.Value.ToTheHour();
        }

        public static DateTime? ToTheMinute(this DateTime? date)
        {
            if (!date.HasValue) {
                return null;
            }

            return date.Value.ToTheMinute();
        }

        public static DateTime? ToTheSecond(this DateTime? date)
        {
            if (!date.HasValue) {
                return null;
            }

            return date.Value.ToTheSecond();
        }

        public static DateTime? ToTheMillisecond(this DateTime? date)
        {
            if (!date.HasValue) {
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

    public class MonthSpan
    {
        public int Interval { get; private set; }

        public MonthSpan(int interval)
        {
            Interval = interval;
        }

        public static implicit operator TimeSpan(MonthSpan v)
        {
            var now = DateTime.Now;
            var then = now.AddMonths(v.Interval);

            return then - now;
        }
    }

    public class QuarterSpan
    {
        public int Interval { get; private set; }

        public QuarterSpan(int interval)
        {
            Interval = interval;
        }

        public static implicit operator TimeSpan(QuarterSpan v)
        {
            var now = DateTime.Now;
            var then = now.AddMonths(3 * v.Interval);

            return then - now;
        }
    }

    public class YearSpan
    {
        public int Interval { get; private set; }

        public YearSpan(int interval)
        {
            Interval = interval;
        }

        public static implicit operator TimeSpan(YearSpan v)
        {
            var now = DateTime.Now;
            var then = now.AddYears(v.Interval);

            return then - now;
        }
    }
}