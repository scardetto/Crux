namespace Crux.Domain.Events
{
	public interface IHandle<in T> where T : IDomainEvent
	{
		void Handle(T args);
	}
}