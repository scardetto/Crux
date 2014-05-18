using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests
{
    [TestFixture]
    public class MoneyDistributorTester
    {
        [Test]
        public void should_distribute_properly()
        {
            var entries = new List<TestEntry>() {
                new TestEntry(1),
                new TestEntry(1),
                new TestEntry(1),
            }.AsEnumerable();

            var adjustments = new MoneyDistributor<TestEntry>(entries)
                .GetAdjustments(1, e => e.Total)
                .ToList();

            adjustments[0].Value.Should().Be(0.33M);
            adjustments[1].Value.Should().Be(0.33M);
            adjustments[2].Value.Should().Be(0.34M);
        }
    }

    public class TestEntry
    {
        public decimal Total { get; set; }

        public TestEntry(decimal total)
        {
            Total = total;
        }
    }
}
