using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Crux.Domain.Persistence.NHibernate.Config
{
    public sealed class TreatStringAsSqlAnsiStringConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            if (instance.Property.PropertyType == typeof(string))
                instance.CustomType("AnsiString");
        }
    }
}
