using System;
using Crux.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests.Extensions
{
    [TestFixture]
    public class GuidExtensionsTester
    {
        private const string GUID_STRING = "2a4bd8ee-d250-40bd-9403-7b5579f98e0b";
        private const string BAD_GUID_STRING = "this is not a guid";
        private static readonly Guid GUID = new Guid(GUID_STRING);

        [Test]
        public void should_convert_string_to_guid()
        {
            var guid = GUID_STRING.ToGuid();
            guid.Should().Be(GUID);
        }

        [Test]
        public void should_return_empty_guid_if_not_parsable()
        {
            var guid = BAD_GUID_STRING.ToGuid();
            guid.Should().Be(Guid.Empty);
        }

        [Test]
        public void should_convert_null_to_empty_guid()
        {
            String nullGuid = null;
            nullGuid.ToGuid().Should().Be(Guid.Empty);
        }

        [Test]
        public void should_convert_guid_to_string()
        {
            var guid = GUID;
            guid.AsString().Should().Be(GUID_STRING);
        }

        [Test]
        public void should_convert_empty_guid_to_empty_string()
        {
            Guid.Empty.AsString().Should().Be(String.Empty);
        }

        [Test]
        public void should_detect_guid_in_string()
        {
            GUID_STRING.IsGuid().Should().BeTrue();
        }

        [Test]
        public void should_detect_bad_guid_in_string()
        {
            BAD_GUID_STRING.IsGuid().Should().BeFalse();
        }

        [Test]
        public void should_throw_when_guid_is_invalid()
        {
            var assertion = new Action(() => BAD_GUID_STRING.AssertGuid());
            assertion.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void should_not_throw_when_guid_is_invalid()
        {
            var assertion = new Action(() => GUID_STRING.AssertGuid());
            assertion.ShouldNotThrow<ArgumentException>();
        }
    }
}
