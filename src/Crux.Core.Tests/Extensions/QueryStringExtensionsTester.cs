using System.Collections.Generic;
using Crux.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests.Extensions
{
    [TestFixture]
    public class QueryStringExtensionsTester
    {
        [Test]
        public void should_convert_string_dictionary_to_query_string()
        {
            var values = new Dictionary<string, string> {
                {"key1", "value1"},
                {"key2", "value2"}
            };

            values.ToQueryString().Should().Be("key1=value1&key2=value2");
        }

        [Test]
        public void should_encode_values()
        {
            var values = new Dictionary<string, string> {
                {"name", "John Smith"},
                {"email", "email@website.com"}
            };

            values.ToQueryString(encode: true)
                .Should().Be("name=John+Smith&email=email%40website.com");
        }

        [Test]
        public void should_override_default_delimiters_when_writing()
        {
            var values = new Dictionary<string, string> {
                {"key1", "value1"},
                {"key2", "value2"}
            };

            values.ToQueryString(paramDelimiter: ' ', valueDelimiter: '@')
                .Should().Be("key1@value1 key2@value2");
        }

        [Test]
        public void should_parse_query_string_to_dictionary()
        {
            var values = "key1=value1&key2=value2".FromQueryString();

            values.Count.Should().Be(2);
            values["key1"].Should().Be("value1");
            values["key2"].Should().Be("value2");
        }

        [Test]
        public void should_decode_values_from_query_string()
        {
            var values = "name=John+Smith&email=email%40website.com"
                .FromQueryString(decode: true);

            values.Count.Should().Be(2);
            values["name"].Should().Be("John Smith");
            values["email"].Should().Be("email@website.com");
        }

        [Test]
        public void should_override_default_delimiters_when_parsing()
        {
            var values = "key1@value1 key2@value2"
                .FromQueryString(paramDelimiter: ' ', valueDelimiter: '@');

            values.Count.Should().Be(2);
            values["key1"].Should().Be("value1");
            values["key2"].Should().Be("value2");
        }
    }
}
