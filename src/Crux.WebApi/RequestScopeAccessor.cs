using System.Web;

namespace Crux.WebApi
{
    public class RequestScopeAccessor<T>
    {
        private readonly string _key;

        public RequestScopeAccessor(string key)
        {
            _key = key;
        }

        public T Get()
        {
            return (T)HttpContext.Current.Items[_key];
        }

        public void Set(T value)
        {
            HttpContext.Current.Items[_key] = value;
        }
    }
}
