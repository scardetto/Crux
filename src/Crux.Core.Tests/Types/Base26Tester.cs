using System;
using Crux.Core.Types;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests.Types
{
    [TestFixture]
    public class Base26Tester
    {
        [TestCase(1, "A")]
        [TestCase(3485, "EDA")]
        [TestCase(3973, "EVU")]
        [TestCase(3999, "EWU")]
        public void should_convert_alphabet_numbers_to_decimal(int decimalNumber, string alphaNumber)
        {
            ((Base26)decimalNumber).Should().Be((Base26)alphaNumber);
        }

        [TestCase("CPU", 2465)]
        [TestCase("CUB", 2576)]
        [TestCase("DID", 2942)]
        public void should_convert_decimal_to_alphabet_number(string alphaNumber, int decimalNumber)
        {
            ((Base26)alphaNumber).Should().Be((Base26)decimalNumber);
        }

        [TestCase("")]          //Empty string
        [TestCase("A1")]        //Numeric values
        [TestCase("á")]         //Non standard letters
        [TestCase("0")]         //Zero
        public void should_throw_exception_for_invalid_alpha_number(string invalidValue)
        {
            Action a = () => new Base26(invalidValue);

            a
                .ShouldThrow<ArgumentException>()
                .WithMessage($"{invalidValue} is not a valid alphaNumber");
        }

        [Test]
        public void should_trap_alpha_numbers_outside_of_int_range()
        {
            Action a = () => new Base26("FXSHRXZ");

            a
                .ShouldThrow<ArgumentException>()
                .WithMessage("alphaNumber given is outside of the range of the integer type");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void should_throw_exception_for_invalid_decimal_number(int invalidValue)
        {
            Action a = () => new Base26(invalidValue);

            a
                .ShouldThrow<ArgumentException>()
                .WithMessage("decimalNumber cannot be 0 or negative");
        }

        [TestCase("A", "A", "B")]
        [TestCase("Z", "A", "AA")]
        public void should_be_able_to_do_some_arithmetic(string a, string b, string result)
        {
            ((Base26) a + (Base26) b).Should().Be((Base26)result);
        }
    }
}