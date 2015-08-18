using System.Collections.Generic;
using System.Collections.ObjectModel;
using Crux.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests.Extensions
{
    [TestFixture]
    public class ReadOnlyDictionaryExtensionsTester
    {
        private readonly IReadOnlyDictionary<string, string> _dictionary = new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { "key", "value" } });

        [Test]
        public void should_get_value_from_dictionary()
        {
            _dictionary.GetValue("key").Should().Be("value");
        }

        [Test]
        public void should_get_the_default_for_type_when_no_key_is_found()
        {
            _dictionary.GetValue("key").Should().BeNull();
        }
    }
}
