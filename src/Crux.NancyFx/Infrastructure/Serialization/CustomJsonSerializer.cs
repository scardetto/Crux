using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Crux.NancyFx.Infrastructure.Serialization
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            DateTimeZoneHandling = DateTimeZoneHandling.Local;
        }
    }
}
