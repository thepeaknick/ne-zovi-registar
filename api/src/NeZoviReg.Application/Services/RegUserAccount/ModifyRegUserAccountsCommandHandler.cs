using System.Runtime.Intrinsics.X86;
using AutoMapper;
using MediatR;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.UserAccount;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;
using Serilog;

namespace NeZoviReg.Application.Services.RegUserAccount;

internal sealed class ModifyRegUserAccountsCommandHandler : ICommandHandler<ModifyRegUserAccountsCommand, bool>
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IRegUserAccountDataStore _regUserAccountDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModifyRegUserAccountsCommandHandler(IRegUserDataStore regUserDataStore,
        IRegUserAccountDataStore regUserAccountDataStore,
        IPublisher publisher,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _regUserDataStore = regUserDataStore;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _regUserAccountDataStore = regUserAccountDataStore;
    }

    public async Task<Result<bool>> Handle(ModifyRegUserAccountsCommand command, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByGuidId(command.RegUserId, cancellationToken);

        if (regUser is null)
        {
            Log.Information($"RegUser with RegUserId={command.RegUserId} does not exist.");

            return Result.Failure<bool>(RegErrors.RegUser.NotFound(command.RegUserId));
        }

        var userAccounts = (await _regUserAccountDataStore.GetUserAccounts(regUser.Id, cancellationToken));

        ModifyUserAccounts(userAccounts, command.Accounts, regUser.Id, command.CurrentUsername);

        await _regUserAccountDataStore.AddOrUpdateUserAccounts(userAccounts, cancellationToken);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserModifiedEvent
        {
            RegUserId = regUser.GuidId
        }, cancellationToken);

        Log.Information($"RegUser with RegUserId={command.RegUserId} modified.");

        return Result.Success(true);
    }


    private void ModifyUserAccounts(List<Domain.Model.Auth.RegUserAccount> accounts, List<UserAccountData> newAccounts,
        int regUserId, string currentUsername)
    {
        var currentUserAccount = newAccounts.FirstOrDefault(x => x.Username == currentUsername);
        if (currentUserAccount != default)
        {
            accounts.First(x => x.Username == currentUsername)
                .WithFirstName(currentUserAccount.FirstName)
                .WithLastName(currentUserAccount.LastName);
        }

        //delete all except the current (logged in)
        accounts
            .Where(x => x.Username != currentUsername)
            .ToList()
            .ForEach(x => x.Delete());


        accounts.AddRange(newAccounts
            .Where(x => x.Username != currentUsername)
            .Select(x => Domain.Model.Auth.RegUserAccount.New
                .WithUserName(x.Username)
                .WithPassword(x.Password)
                .WithFirstName(x.FirstName)
                .WithLastName(x.LastName)
                .WithRegUserId(regUserId)));
    }
}