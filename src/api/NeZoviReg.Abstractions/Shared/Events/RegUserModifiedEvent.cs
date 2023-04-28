using MediatR;

namespace NeZoviReg.Abstractions.Shared.Events;

public record RegUserModifiedEvent : INotification
{
    public Guid RegUserId { get; init; }
}
