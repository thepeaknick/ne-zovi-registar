namespace NeZoviReg.WebApi.Model.User;

public record AddUserRequest(string FirstName, string LastName, string Jmbg, string PhoneNumber);