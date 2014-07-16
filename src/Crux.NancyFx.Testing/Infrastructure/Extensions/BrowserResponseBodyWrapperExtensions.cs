using System;
using System.Linq;
using System.Text;
using Nancy.Testing;

namespace Crux.NancyFx.Testing.Infrastructure.Extensions
{
    public static class BrowserResponseBodyWrapperExtensions
    {
        public static string StringContent(this BrowserResponseBodyWrapper body)
        {
            if (body == null) throw new ArgumentNullException("body");

            return Encoding.Default.GetString(body.ToArray());
        }
    }
}
