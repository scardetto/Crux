using System;
using Crux.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests.Extensions
{
    public enum TestEnum1
    {
        Foo,
        Bar
    }

    [TestFixture]
    public class EnumExtentionsTester
    {
        [Test]
        public void should_return_enum_when_given_string()
        {
            var result = "foo".AsEnum<TestEnum1>();
            result.Should().Be(TestEnum1.Foo);

            result = "Foo".AsEnum<TestEnum1>();
            result.Should().Be(TestEnum1.Foo);

            result = "BAR".AsEnum<TestEnum1>();
            result.Should().Be(TestEnum1.Bar);
        }

        [Test]
        public void should_throw_exception_for_non_enum_type()
        {
            Assert.Throws<InvalidCastException>(() => "foobar".AsEnum<EnumExtentionsTester>(), "This type can only be cast to an Enum type.");
        }

        [Test]
        public void should_return_enum_when_given_an_integer()
        {
            var result = 0.AsEnum<TestEnum1>();
            result.Should().Be(TestEnum1.Foo);

            result = 1.AsEnum<TestEnum1>();
            result.Should().Be(TestEnum1.Bar);
        }

        [Test]
        public void should_raise_exception_when_name_not_in_enum()
        {
            Assert.Throws<InvalidCastException>(() => "foobar".AsEnum<TestEnum1>(), "foobar is not a defined value of TestEnum1");
            Assert.Throws<InvalidCastException>(() => 3.AsEnum<TestEnum1>(), "3 is not a defined value of TestEnum1");
        }

        [Test]
        public void should_validate_string_value_is_valid_enum_value()
        {
            "foo".IsValidValue<TestEnum1>().Should().BeTrue();
            "foobar".IsValidValue<TestEnum1>().Should().BeFalse();
        }
    }
}
