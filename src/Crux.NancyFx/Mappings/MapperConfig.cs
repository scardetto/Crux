using Crux.Core.Bootstrapping;

namespace Crux.NancyFx.Mappings
{
    public abstract class MapperConfig : IRunAtStartup
    {
        public void Init()
        {
            ToModel();
            ToDomain();
        }

        protected virtual void ToDomain() { }

        protected virtual void ToModel() { }
    }
}
