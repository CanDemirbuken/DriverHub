using DriverHub.Application.Contracts.Communication.Mail;

namespace DriverHub.Application.Interfaces.Communication;

public sealed record PendingEmail(Guid Id, SendMailRequest Mail);

public interface IEmailOutbox
{
    // Caller holds a transaction through delivery and acknowledgement.
    Task<PendingEmail?> GetNextForDeliveryAsync(CancellationToken cancellationToken);
    Task MarkDeliveredAsync(Guid id, CancellationToken cancellationToken);
    Task ScheduleRetryAsync(Guid id, CancellationToken cancellationToken);
}
