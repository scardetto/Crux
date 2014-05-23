using System.IO;
using Crux.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests.Extensions
{
    [TestFixture]
    public class TelcoExtensionsTester
    {
        [Test]
        public void should_validate_us_phone_number()
        {
            "5617774825".IsUSPhoneNumber().Should().Be(true);
            "(281)388-0388".IsUSPhoneNumber().Should().Be(true);
            "(281)388-0300".IsUSPhoneNumber().Should().Be(true);
            "(979) 778-0978".IsUSPhoneNumber().Should().Be(true);
            "(281)934-2479".IsUSPhoneNumber().Should().Be(true);
            "(281)934-2447".IsUSPhoneNumber().Should().Be(true);
            "(979)826-3273".IsUSPhoneNumber().Should().Be(true);
            "(979)826-3255".IsUSPhoneNumber().Should().Be(true);
            "1334714149".IsUSPhoneNumber().Should().Be(true);
            "(281)356-2530".IsUSPhoneNumber().Should().Be(true);
            "(281)356-5264".IsUSPhoneNumber().Should().Be(true);
            "(936)825-2081".IsUSPhoneNumber().Should().Be(true);
            "(832)595-9500".IsUSPhoneNumber().Should().Be(true);
            "(832)595-9501".IsUSPhoneNumber().Should().Be(true);
            "281-342-2452".IsUSPhoneNumber().Should().Be(true);
            "1334431660".IsUSPhoneNumber().Should().Be(true);
            "1334431660222".IsUSPhoneNumber().Should().Be(false);
        }

        [Test]
        public void should_thorw_exception_for_invalid_US_number()
        {
            Assert.Throws<InvalidDataException>(() => "foobar".AssertValidUSPhoneNumber(), "foobar is not a valid US phone number");
            Assert.Throws<InvalidDataException>(() => "561777482".AssertValidUSPhoneNumber(), "561777482 is not a valid US phone number");
            Assert.Throws<InvalidDataException>(() => "13344316601".AssertValidUSPhoneNumber(), "13344316601 is not a valid US phone number");
        }
    }
}
