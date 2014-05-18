namespace Crux.Caching
{
    public class ValuePlaceholder : ICachePlaceholder
    {
        public object Value { get; private set; }

        public ValuePlaceholder(object value)
        {
            Value = value;
        }
    }
}