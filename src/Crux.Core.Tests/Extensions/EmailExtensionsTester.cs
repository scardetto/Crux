using Crux.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Crux.Core.Tests.Extensions
{
    [TestFixture]
   public class EmailExtensionsTester
    {
        [Test]
        public void should_validate_email_address()
        {
            "jhondoe@yahoo.com".IsEmailAddress().Should().BeTrue();
            "jhon.doe@yahoo.com".IsEmailAddress().Should().BeTrue();
            "jhondoe@yahoo.com".IsEmailAddress().Should().BeTrue();
            "jhon.doe@yahoo.net".IsEmailAddress().Should().BeTrue();
            "jhondoe@yahoo.org".IsEmailAddress().Should().BeTrue();
            "jhondoe@yahoo.us".IsEmailAddress().Should().BeTrue();
            "jhondoe@yahoo.it".IsEmailAddress().Should().BeTrue();

        }
    }
}
