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

    public static IRuleBuilderOptions<TObjType, string> EmailFormat<TObjType, TCommandReturnType>(this IRuleBuilder<TObjType, string> ruleBuilder, string message)
        where TObjType : ICommand<TCommandReturnType>
    {
        return ruleBuilder.EmailAddress()
            .WithErrorCode(ErrorCode.InvalidFormat.ToString())
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<TObjType, string> EmailFormat<TObjType>(this IRuleBuilder<TObjType, string> ruleBuilder, string message)
        where TObjType : ICommand
    {
        return ruleBuilder.EmailAddress()
            .WithErrorCode(ErrorCode.InvalidFormat.ToString())
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<TObjType, string> RegexFormat<TObjType, TCommandReturnType>(this IRuleBuilder<TObjType, string> ruleBuilder, string regex, string message)
        where TObjType : ICommand<TCommandReturnType>
    {
        return ruleBuilder.Matches(regex)
            .WithErrorCode(ErrorCode.InvalidFormat.ToString())
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<TObjType, string> RegexFormat<TObjType>(this IRuleBuilder<TObjType, string> ruleBuilder, string regex, string message)
        where TObjType : ICommand
    {
        return ruleBuilder.Matches(regex)
            .WithErrorCode(ErrorCode.InvalidFormat.ToString())
            .WithMessage(message);
    }
}