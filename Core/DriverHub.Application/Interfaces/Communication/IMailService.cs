using DriverHub.Application.Contracts.Communication.Mail;

namespace DriverHub.Application.Interfaces.Communication;

public interface IMailService
{
    Task SendAsync(SendMailRequest request, CancellationToken cancellationToken = default);
}