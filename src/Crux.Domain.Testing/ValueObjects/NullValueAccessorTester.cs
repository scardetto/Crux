using Crux.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Domain.Testing.ValueObjects
{
    [TestFixture]
    public class NullValueAccessorTester
    {
        private ObjectWithNullAccessor _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new ObjectWithNullAccessor();
        }

        [Test]
        public void should_set_backing_field_to_null()
        {
            _subject.TestObject = null;
            _subject._testObject.Should().BeNull();
            _subject.TestObject.Should().Be(NullTestObject.Instance);
        }

        [Test]
        public void should_return_null_object_via_public_interface()
        {
            _subject.TestObject = null;
            _subject.TestObject.Should().Be(NullTestObject.Instance);
        }

        private class ObjectWithNullAccessor
        {
            private readonly NullValueAccessor<TestObject> _accessor = 
                new NullValueAccessor<TestObject>(NullTestObject.Instance);

            public TestObject _testObject;

            public TestObject TestObject
            {
                get { return _accessor.Get(_testObject); }
                set { _testObject = _accessor.Set(value); }
            }
        }
    }
}
