namespace NeZoviReg.Abstractions.Messaging.Auth.Commands;

public record ForgotPassCommand(string Email, string HtmlTemplatePath) : BaseCommand<string>;
