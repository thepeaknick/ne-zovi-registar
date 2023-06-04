using System.Net.Mail;

namespace NeZoviReg.Abstractions.Email;

public static class EmailExtensions
{
    public static async Task<bool> SendEmailAsync(this IEmailSender sender, string emailFrom, string subject, string content, CancellationToken cancellationToken)
    {
        using var message = new MailMessage
        {
            Body = content,
            From = new MailAddress(emailFrom),
            Subject = subject
        };

        return await sender.SendEmailAsync(message, cancellationToken);
    }

    private static void AddRange(this MailAddressCollection collection, IEnumerable<MailAddress> addresses)
    {
        foreach (var address in addresses ?? Enumerable.Empty<MailAddress>())
            collection.Add(address);
    }


    private static IEnumerable<MailAddress> ParseEmailAddresses(this string emailAddresses, char[]? separator = default)
    {
        if (string.IsNullOrWhiteSpace(emailAddresses))
            return Enumerable.Empty<MailAddress>();

        separator ??= new[] { ';', ',' };

        return emailAddresses
            .Split(separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => new MailAddress(x.Trim()));
    }


}