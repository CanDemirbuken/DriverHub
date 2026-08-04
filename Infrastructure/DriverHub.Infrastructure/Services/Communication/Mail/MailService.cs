using DriverHub.Application.Contracts.Communication.Mail;
using DriverHub.Application.Interfaces.Communication;
using DriverHub.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DriverHub.Infrastructure.Services.Communication.Mail;

public sealed class MailService(IOptions<SmtpOptions> options) : IMailService
{
    private readonly SmtpOptions _smtpOptions = options.Value;

    public async Task SendAsync(SendMailRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.To);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Subject);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Body);

        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(_smtpOptions.FromName, _smtpOptions.FromEmail));
        message.To.Add(MailboxAddress.Parse(request.To));
        message.Subject = request.Subject;

        var bodyBuilder = new BodyBuilder();

        if (request.IsHtml)
            bodyBuilder.HtmlBody = request.Body;
        else
            bodyBuilder.TextBody = request.Body;

        message.Body = bodyBuilder.ToMessageBody();

        using var client = new MailKit.Net.Smtp.SmtpClient();

        try
        {
            await client.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, _smtpOptions.SecureSocketOption, cancellationToken);
            await client.AuthenticateAsync(_smtpOptions.UserName, _smtpOptions.Password, cancellationToken);
            await client.SendAsync(message, cancellationToken);
        }
        finally
        {
            if (client.IsConnected)
                await client.DisconnectAsync(true, CancellationToken.None);
        }
    }
}