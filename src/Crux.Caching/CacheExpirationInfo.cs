namespace Crux.Caching
{
    public class CacheExpirationInfo
    {
        public string Type { get; set; }
        public string Param { get; set; }

        public CacheExpirationInfo(string type, string param)
        {
            Type = type;
            Param = param;
        }
    }
}