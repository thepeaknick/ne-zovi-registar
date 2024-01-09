using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Email;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Options;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Model.Auth;
using NeZoviReg.Auth.Authentication.Jwt;
using Serilog;

namespace NeZoviReg.Auth.Services.Login;

internal sealed class ForgotPasswordCommandHandler : ICommandHandler<ForgotPassCommand, string>
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IJwtProvider _jwtProvider;
    private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ForgotPasswordOptions _options;

    public ForgotPasswordCommandHandler(
        IRegUserDataStore regUserDataStore,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork,
        IEmailSender emailSender, 
        IOptions<ForgotPasswordOptions> options)
    {
        _regUserDataStore = regUserDataStore;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
        _options = options.Value;
    }

    public async Task<Result<string>> Handle(ForgotPassCommand command, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByEmail(command.Email, cancellationToken);

        if (regUser is null)
        {
            Log.Information($"RegUser with Email={command.Email} does not exist.");
            
            return Result.Failure<string>(RegErrors.RegUser.Unknown);
        }
        
        var tokenResult = await _jwtProvider.GenerateTokenAsync(regUser, _options.TokenExpirationInMinutes, cancellationToken);

        var htmlContent = await CreateEmailBody(tokenResult);

        if (!await _emailSender.SendEmailAsync(command.Email, _options.Subject!, htmlContent, true,
                cancellationToken)) 
            return Result.Failure<string>(RegErrors.RegUser.EmailNotSent);
        
        regUser.WithForgotPasswordToken(tokenResult.AccessToken)
            .WithForgotPasswordTokenExpTime(tokenResult.AccessTokenExpTime);

        _regUserDataStore.Update(regUser);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        Log.Information($"RegUser with Email={command.Email} changed forgotten password.");
        
        return tokenResult.AccessToken;

    }

    private async Task<string> CreateEmailBody(TokenResult tokenResult)
    {
        using StreamReader SourceReader = File.OpenText(_options.HtmlTemplatePath!);
        var body = await SourceReader.ReadToEndAsync();
        
        body = body.Replace("{Link}", $"{_options.CallBackUrl}?token={tokenResult.AccessToken}");
        return body;
    }
}
