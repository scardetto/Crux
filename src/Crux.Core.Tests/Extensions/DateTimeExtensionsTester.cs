using System;
using Crux.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTester
    {
        [Test]
        public void ToTheSecond_should_truncate_milliseconds()
        {
            var dateTime = new DateTime(2014, 6, 9, 11, 53, 15, 150);

            var dateTimeToTheSecond = dateTime.ToTheSecond();

            dateTimeToTheSecond.Year.Should().Be(dateTime.Year);
            dateTimeToTheSecond.Month.Should().Be(dateTime.Month);
            dateTimeToTheSecond.Day.Should().Be(dateTime.Day);
            dateTimeToTheSecond.Hour.Should().Be(dateTime.Hour);
            dateTimeToTheSecond.Minute.Should().Be(dateTime.Minute);
            dateTimeToTheSecond.Second.Should().Be(dateTime.Second);
            dateTimeToTheSecond.Millisecond.Should().Be(0);
        }
    }
}
