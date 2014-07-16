using Owin;

namespace Crux.NancyFx.Bootstrapping
{
    public static class Bootstrapper
    {
        public static void Bootstrap(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
