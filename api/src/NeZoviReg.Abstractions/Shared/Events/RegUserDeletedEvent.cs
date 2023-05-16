using MediatR;

namespace NeZoviReg.Abstractions.Shared.Events;

public record RegUserDeletedEvent : INotification
{
    public Guid RegUserId { get; init; }
}
