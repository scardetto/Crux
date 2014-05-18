using System;

namespace Crux.Core.Extensions
{
    public static class RoundingExtensions
    {
        public static decimal RoundToCurrency(this decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.ToEven);
        }
    }
}
