using Nancy;
using Nancy.Security;

namespace Crux.NancyFx.Modules
{
    public class SecuredModule : NancyModule
    {
        public SecuredModule()
        {
            this.RequiresAuthentication();
        }
    }
}
