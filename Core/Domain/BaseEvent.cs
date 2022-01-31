using MediatR;

namespace Core.Domain;

public class BaseEvent : INotification
{
	public DateTime TimeOccured { get; set; }
}
