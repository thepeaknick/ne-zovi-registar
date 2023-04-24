using FluentValidation;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Shared.Enums;

namespace NeZoviReg.Abstractions.Extensions;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<TObjType, TPropType> NotEmpty<TObjType, TPropType, TCommandReturnType>(this IRuleBuilder<TObjType, TPropType> ruleBuilder, string message)
        where TObjType : ICommand<TCommandReturnType>
    {
        return ruleBuilder.NotEmpty()
            .WithErrorCode(ErrorCode.Empty.ToString())
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<TObjType, TPropType> NotEmpty<TObjType, TPropType>(this IRuleBuilder<TObjType, TPropType> ruleBuilder, string message)
        where TObjType : ICommand
    {
        return ruleBuilder.NotEmpty()
            .WithErrorCode(ErrorCode.Empty.ToString())
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<TObjType, string> MaximumLength<TObjType, TCommandReturnType>(this IRuleBuilder<TObjType, string> ruleBuilder, int maximumLength, string message)
        where TObjType : ICommand<TCommandReturnType>
    {
        return ruleBuilder.MaximumLength(maximumLength)
            .WithErrorCode(ErrorCode.TooLong.ToString())
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<TObjType, string> MaximumLength<TObjType>(this IRuleBuilder<TObjType, string> ruleBuilder, int maximumLength, string message)
        where TObjType : ICommand
    {
        return ruleBuilder.MaximumLength(maximumLength)
            .WithErrorCode(ErrorCode.TooLong.ToString())
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<TObjType, TPropType> InvalidFormat<TObjType, TPropType, TCommandReturnType>(this IRuleBuilder<TObjType, TPropType> ruleBuilder, string message, Func<TPropType, bool> action)
        where TObjType : ICommand<TCommandReturnType>
    {
        return ruleBuilder.Must(action)
            .WithErrorCode(ErrorCode.InvalidFormat.ToString())
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<TObjType, TPropType> InvalidFormat<TObjType, TPropType>(this IRuleBuilder<TObjType, TPropType> ruleBuilder, string message, Func<TPropType, bool> action)
        where TObjType : ICommand
    {
        return ruleBuilder.Must(action)
            .WithErrorCode(ErrorCode.InvalidFormat.ToString())
            .WithMessage(message);
    }
}