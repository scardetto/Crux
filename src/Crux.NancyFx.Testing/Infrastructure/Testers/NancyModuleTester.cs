using Nancy.Bootstrapper;
using Nancy.Testing;

namespace Crux.NancyFx.Testing.Infrastructure.Testers
{
    public abstract class NancyModuleTester
    {
        protected NancyModuleTester(INancyBootstrapper bootstrapper)
        {
            Browser = new Browser(bootstrapper); 
        }

        protected readonly Browser Browser;
    }
}
