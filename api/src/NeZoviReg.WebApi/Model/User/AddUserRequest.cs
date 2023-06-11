namespace NeZoviReg.WebApi.Model.User;

public record AddUserRequest(string FirstName, string LastName, string Jmbg, int OperatorId, string[] PhoneNumbers);