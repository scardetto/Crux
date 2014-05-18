namespace Crux.Domain.ValueObjects
{
	public class NullValueAccessor<T>
		where T : class, INullable
	{
		private readonly T _nullValue;

		public NullValueAccessor(T nullValue)
		{
			_nullValue = nullValue;
		}

		public T Get(T realValue)
		{
			return realValue ?? _nullValue;
		}

		public T Set(T newValue)
		{
		    return newValue == _nullValue ? null : newValue;
		}
	}
}
