namespace Crux.Caching
{
    public interface ICachePlaceholder
    {
        object Value { get; }
    }

    public static class CachePlaceholderExtensions
    {
        public static object UnwrapPlaceholder(this object value)
        {
            var placeholder = value as ICachePlaceholder;

            return placeholder == null
                ? value
                : placeholder.Value;
        }
    }
}