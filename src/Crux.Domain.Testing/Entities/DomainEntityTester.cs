using Crux.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Domain.Testing.Entities
{
    [TestFixture]
    public class DomainEntityTester
    {
        [Test]
        public void should_be_equal_if_ids_are_equal()
        {
            var entity1 = new TestDomainObject { ID = 1 };
            var entity2 = new TestDomainObject { ID = 1 };

            entity1.Should().Be(entity2);
        }

        [Test]
        public void should_not_be_equal_if_ids_are_different()
        {
            var entity1 = new TestDomainObject { ID = 1 };
            var entity2 = new TestDomainObject { ID = 2 };

            entity1.Should().NotBe(entity2);
        }

        private class TestDomainObject : DomainEntityOfId<int> { }
    }
}
