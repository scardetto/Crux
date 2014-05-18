using System;
using System.Globalization;
using System.Text;
using Crux.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTester
    {
        [Test]
        public void should_be_blank_when_null_or_empty()
        {
            var value = "";
            value.IsBlank().Should().BeTrue();

            // ReSharper disable ExpressionIsAlwaysNull
            value = null;
            value.IsBlank().Should().BeTrue();
            // ReSharper restore ExpressionIsAlwaysNull

            value = "string";
            value.IsBlank().Should().BeFalse();
        }

        [Test]
        public void should_be_present_when_not_blank()
        {
            var value = "";
            value.IsPresent().Should().BeFalse();

            // ReSharper disable ExpressionIsAlwaysNull
            value = null;
            value.IsPresent().Should().BeFalse();
            // ReSharper restore ExpressionIsAlwaysNull

            value = "string";
            value.IsPresent().Should().BeTrue();
        }

        [Test]
        public void should_all_be_present()
        {
            var values = new[] {"these", "are", "all", "present"};
            values.ArePresent().Should().BeTrue();

            values = new[] {"", null};
            values.ArePresent().Should().BeFalse();

            values = new[] { "one", "value", "is", null };
            values.ArePresent().Should().BeFalse();
        }

        [Test]
        public void should_all_be_blank()
        {
            var values = new[] { "", null };
            values.AreBlank().Should().BeTrue();

            values = new[] { "these", "are", "all", "present" };
            values.AreBlank().Should().BeFalse();
        }

        [Test]
        public void should_be_numeric()
        {
            "1".IsNumeric().Should().BeTrue();
            "1.0".IsNumeric().Should().BeTrue();
            "$1.00".IsNumeric().Should().BeTrue();
            "text".IsNumeric().Should().BeFalse();

            var culture = CultureInfo.GetCultureInfo("en-US");
            "100,000.00".IsNumeric(culture).Should().BeTrue();

            culture = CultureInfo.GetCultureInfo("en-GB");
            "100000,00".IsNumeric(culture).Should().BeTrue();
        }

        [Test]
        public void should_be_alphanumeric()
        {
            "1".IsAlphanumeric().Should().BeTrue();
            "A1".IsAlphanumeric().Should().BeTrue();
            "John Smith".IsAlphanumeric().Should().BeFalse("it has spaces");
            "this_has_symbols.".IsAlphanumeric().Should().BeFalse();
            "$1.00".IsAlphanumeric().Should().BeFalse("it has non alphanumeric characters");
        }

        [Test]
        public void should_be_alphabetic()
        {
            "ABCDE".IsAlphabetic().Should().BeTrue();
            "12345".IsAlphabetic().Should().BeFalse();
            "A1B2C3D4E5".IsAlphabetic().Should().BeFalse();
            "A.B,C_E".IsAlphabetic().Should().BeFalse();
        }

        [Test]
        public void should_match_all_chars()
        {
            "ABCDE".AllCharsMatch(Char.IsLetter).Should().BeTrue();
            "12345".AllCharsMatch(Char.IsDigit).Should().BeTrue();

            "ABCDE".AllCharsMatch(Char.IsDigit).Should().BeFalse();
            "12345".AllCharsMatch(Char.IsLetter).Should().BeFalse();
        }

        [Test]
        public void should_strip_all_chars()
        {
            "A1B2C3D4E5".StripChars(Char.IsLetter).Should().Be("12345");
            "A1B2C3D4E5".StripChars(Char.IsDigit).Should().Be("ABCDE");
        }

        [Test]
        public void should_encode_and_decode()
        {
            "this is a test".Encode().Decode().Should().Be("this is a test");
            "this is a test".Encode(Encoding.UTF8).Decode(Encoding.UTF8).Should().Be("this is a test");
        }

        [Test]
        public void should_compare_ignoring_case()
        {
            "this is a test".EqualsIgnoreCase("THIS IS A TEST").Should().BeTrue();
        }

        [Test]
        public void should_capitalize()
        {
            "this is a test".Capitalize().Should().Be("This Is A Test");
        }

        [Test]
        public void should_get_string_from_the_left()
        {
            "12345".Left(1).Should().Be("1");
            "12345".Left(2).Should().Be("12");
            "12345".Left(3).Should().Be("123");
            "12345".Left(4).Should().Be("1234");
            "12345".Left(5).Should().Be("12345");

            // No overflow error
            "12345".Left(6).Should().Be("12345");
        }

        [Test]
        public void should_get_string_from_the_right()
        {
            "12345".Right(1).Should().Be("5");
            "12345".Right(2).Should().Be("45");
            "12345".Right(3).Should().Be("345");
            "12345".Right(4).Should().Be("2345");
            "12345".Right(5).Should().Be("12345");

            // No overflow error
            "12345".Right(6).Should().Be("12345");
        }

        [Test]
        public void should_join_strings()
        {
            var values = new[] { "a", "sequence", "of", "strings" };
            values.Join(",").Should().Be("a,sequence,of,strings");
            values.Join("|").Should().Be("a|sequence|of|strings");
        }
    }
}
