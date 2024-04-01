using MediatR;

namespace NeZoviReg.Abstractions.Shared.Events;

public record RegUserCreatedEvent : INotification
{
    public string CompanyName { get; init; }
    
    public string Email { get; init; }
    
    public string Username { get; init; }
}
