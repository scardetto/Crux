using FluentAssertions;
using NUnit.Framework;

namespace Crux.Domain.Testing.ValueObjects
{
    [TestFixture]
    public class NullableTester
    {
        [Test]
        public void should_demonstrate_the_appropriate_usage()
        {
            var testObject = new TestObject();
            var nullTestObject = NullTestObject.Instance;

            testObject.IsNull.Should().BeFalse();
            nullTestObject.IsNull.Should().BeTrue();
        }
    }
}
