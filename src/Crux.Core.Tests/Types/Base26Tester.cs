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
            ((Base26) decimalNumber).Should().Be((Base26) alphaNumber);
        }

        [TestCase("CPU", 2465)]
        [TestCase("CUB", 2576)]
        [TestCase("DID", 2942)]
        public void should_conert_decimal_to_alphabet_number(string alphaNumber, int decimalNumber)
        {
            ((Base26)alphaNumber).Should().Be((Base26)decimalNumber);
        }

        [TestCase("A", "A", "B")]
        [TestCase("Z", "A", "AA")]
        public void should_be_able_to_do_some_arithmetic(string a, string b, string result)
        {
            ((Base26) a + (Base26) b).Should().Be((Base26)result);
        }
    }
}