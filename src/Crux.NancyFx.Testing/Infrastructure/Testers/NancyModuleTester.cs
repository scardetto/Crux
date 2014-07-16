using System;
using Nancy.Bootstrapper;
using Nancy.Testing;

namespace Crux.NancyFx.Testing.Infrastructure.Testers
{
    public abstract class NancyModuleTester
    {
        protected NancyModuleTester(Action<ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator> action, Action<BrowserContext> defaults = null)
        {
            Browser = new Browser(action, defaults);
        }

        protected NancyModuleTester(INancyBootstrapper bootstrapper, Action<BrowserContext> defaults = null)
        {
            Browser = new Browser(bootstrapper, defaults);
        }

        protected readonly Browser Browser;
    }
}
