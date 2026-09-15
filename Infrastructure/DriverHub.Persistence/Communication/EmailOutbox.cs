using DriverHub.Application.Common.EmailTemplates;
using DriverHub.Application.Contracts.Communication.Mail;
using DriverHub.Application.Interfaces.Communication;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.Communication;

public sealed class EmailOutbox(AppDbContext context) : IReservationNotificationQueue, IEmailOutbox
{
    public async Task EnqueueAsync(Reservation reservation, string vehicle, string pickupLocation, CancellationToken cancellationToken = default)
    {
        // Older accounts may have no usable email. Never invent a recipient.
        if (string.IsNullOrWhiteSpace(reservation.CustomerEmail)) return;
        SendMailRequest mail = ReservationEmailTemplate.Create(reservation, vehicle, pickupLocation);
        await context.Set<EmailOutboxMessage>().AddAsync(new EmailOutboxMessage
        {
            ReservationId = reservation.Id, Status = reservation.Status,
            Recipient = mail.To, Subject = mail.Subject, Body = mail.Body,
            CreatedAt = DateTime.UtcNow, NextAttemptAt = DateTime.UtcNow
        }, cancellationToken);
    }

    public async Task<PendingEmail?> GetNextForDeliveryAsync(CancellationToken cancellationToken)
    {
        if (context.Database.CurrentTransaction is null)
            throw new InvalidOperationException("Outbox delivery requires a transaction.");
        DateTime now = DateTime.UtcNow;
        // Lock one message across workers. Earlier messages for the same reservation are delivered first.
        var messages = await context.Set<EmailOutboxMessage>().FromSqlInterpolated($"""
            SELECT TOP (1) m.* FROM [EmailOutboxMessages] m WITH (UPDLOCK, READPAST, READCOMMITTEDLOCK)
            WHERE m.[DeliveredAt] IS NULL AND m.[NextAttemptAt] <= {now}
              AND NOT EXISTS (SELECT 1 FROM [EmailOutboxMessages] older
                WHERE older.[ReservationId] = m.[ReservationId] AND older.[DeliveredAt] IS NULL
                  AND older.[Status] < m.[Status])
            ORDER BY m.[NextAttemptAt], m.[CreatedAt], m.[Id]
            """).ToListAsync(cancellationToken);
        EmailOutboxMessage? message = messages.SingleOrDefault();
        return message is null ? null : new PendingEmail(message.Id, new SendMailRequest(message.Recipient, message.Subject, message.Body));
    }

    public async Task MarkDeliveredAsync(Guid id, CancellationToken cancellationToken)
    {
        EmailOutboxMessage message = (await context.Set<EmailOutboxMessage>().FindAsync([id], cancellationToken))!;
        message.Attempts++;
        message.DeliveredAt = DateTime.UtcNow;
        // Keep the delivery receipt, remove the duplicated customer data after successful delivery.
        message.Recipient = "";
        message.Body = "";
    }

    public async Task ScheduleRetryAsync(Guid id, CancellationToken cancellationToken)
    {
        EmailOutboxMessage message = (await context.Set<EmailOutboxMessage>().FindAsync([id], cancellationToken))!;
        message.Attempts++;
        message.NextAttemptAt = DateTime.UtcNow.AddMinutes(Math.Min(360, Math.Pow(2, Math.Min(message.Attempts, 9))));
    }
}
