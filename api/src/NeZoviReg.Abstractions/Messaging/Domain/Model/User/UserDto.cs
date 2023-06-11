namespace NeZoviReg.Abstractions.Messaging.Domain.Model.User;

public sealed record UserDto(string PhoneNumber, DateTime? CreatedModifiedOn);