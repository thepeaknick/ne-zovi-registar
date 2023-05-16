using FluentValidation;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Shared.Enums;

namespace NeZoviReg.Abstractions.Extensions;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<TObjType, TPropType> NotEmpty<TObjType, TPropType, TCommandReturnType>(this IRuleBuilder<TObjType, TPropType> ruleBuilder, string message, ErrorCode? code = null)
        where TObjType : ICommand<TCommandReturnType>
    {
        var rb = ruleBuilder.NotEmpty()
            .WithMessage(message);
        if (code != default)
            rb.WithErrorCode(code.ToString());
        return rb;
    }

    public static IRuleBuilderOptions<TObjType, TPropType> NotEmpty<TObjType, TPropType>(this IRuleBuilder<TObjType, TPropType> ruleBuilder, string message, ErrorCode? code = null)
        where TObjType : ICommand
    {
        var rb = ruleBuilder.NotEmpty()
            .WithMessage(message);
        if (code != default)
            rb.WithErrorCode(code.ToString());
        return rb;
    }

    public static IRuleBuilderOptions<TObjType, string> MaximumLength<TObjType, TCommandReturnType>(this IRuleBuilder<TObjType, string> ruleBuilder, int maximumLength, string message, ErrorCode? code = null)
        where TObjType : ICommand<TCommandReturnType>
    {
        var rb = ruleBuilder.MaximumLength(maximumLength)
            .WithMessage(message);

        if (code != default)
            rb.WithErrorCode(code.ToString());
        return rb;
    }

    public static IRuleBuilderOptions<TObjType, string> MaximumLength<TObjType>(this IRuleBuilder<TObjType, string> ruleBuilder, int maximumLength, string message, ErrorCode? code = null)
        where TObjType : ICommand
    {
        var rb = ruleBuilder.MaximumLength(maximumLength)
            .WithMessage(message);

        if (code != default)
            rb.WithErrorCode(code.ToString());
        return rb;
    }

    public static IRuleBuilderOptions<TObjType, TPropType> InvalidFormat<TObjType, TPropType, TCommandReturnType>(this IRuleBuilder<TObjType, TPropType> ruleBuilder, string message, Func<TPropType, bool> action, ErrorCode? code = null)
        where TObjType : ICommand<TCommandReturnType>
    {
        var rb = ruleBuilder.Must(action)
            .WithMessage(message);

        if (code != default)
            rb.WithErrorCode(code.ToString());
        return rb;
    }

    public static IRuleBuilderOptions<TObjType, TPropType> InvalidFormat<TObjType, TPropType>(this IRuleBuilder<TObjType, TPropType> ruleBuilder, string message, Func<TPropType, bool> action, ErrorCode? code = null)
        where TObjType : ICommand
    {
        var rb = ruleBuilder.Must(action)
            .WithMessage(message);

        if (code != default)
            rb.WithErrorCode(code.ToString());
        return rb;
    }

    public static IRuleBuilderOptions<TObjType, string> RegexFormat<TObjType, TCommandReturnType>(this IRuleBuilder<TObjType, string> ruleBuilder, string regex, string message, ErrorCode? code = null)
        where TObjType : ICommand<TCommandReturnType>
    {
        var rb = ruleBuilder.Matches(regex)
            .WithMessage(message);

        if (code != default)
            rb.WithErrorCode(code.ToString());
        return rb;
    }

    public static IRuleBuilderOptions<TObjType, string> RegexFormat<TObjType>(this IRuleBuilder<TObjType, string> ruleBuilder, string regex, string message, ErrorCode? code = null)
        where TObjType : ICommand
    {
        var rb = ruleBuilder.Matches(regex)
            .WithMessage(message);

        if (code != default)
            rb.WithErrorCode(code.ToString());
        return rb;
    }
}