using Core.Domain;

namespace Core.Domains;

public class BaseEntity<TId>
{
	public TId Id { get; set; }
	public List<BaseEvent> Events;
}
