using System;
using Crux.Core.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests.Utils
{
    [TestFixture]
    public class EnumUtilsTests
    {
        private enum TestEnum
        {
            One = 1,
            Two,
            Three
        }

        [Test]
        public void EnumToIdValuePair_should_throw_when_not_enum()
        {
            Action action = () => EnumUtils.ToDictionary<int>();

            action
                .ShouldThrow<ArgumentException>()
                .WithMessage("Type must be an enumeration");
        }

        [Test]
        public void EnumToDictionary_should_return_correctly()
        {
            var dictionary = EnumUtils.ToDictionary<TestEnum>();

            dictionary.Should()
                .Contain(1, "One")
                .And.Contain(2, "Two")
                .And.Contain(3, "Three");
        }
    }
}
