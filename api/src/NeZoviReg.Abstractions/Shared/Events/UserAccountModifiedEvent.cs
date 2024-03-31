using MediatR;

namespace NeZoviReg.Abstractions.Shared.Events;

public record UserAccountModifiedEvent : INotification
{
    public Guid UserAccountId { get; init; }
}
