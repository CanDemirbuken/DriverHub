using DriverHub.Application.Interfaces.Communication;
using DriverHub.Application.Interfaces.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DriverHub.Infrastructure.Services.Communication.Mail;

public sealed class EmailOutboxWorker(IServiceScopeFactory scopeFactory, ILogger<EmailOutboxWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            bool processed = false;
            try
            {
                await using AsyncServiceScope scope = scopeFactory.CreateAsyncScope();
                var outbox = scope.ServiceProvider.GetRequiredService<IEmailOutbox>();
                var mail = scope.ServiceProvider.GetRequiredService<IMailService>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                await using var transaction = await unitOfWork.BeginTransactionAsync(stoppingToken);
                PendingEmail? message = await outbox.GetNextForDeliveryAsync(stoppingToken);
                if (message is not null)
                {
                    using var timeout = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
                    timeout.CancelAfter(TimeSpan.FromSeconds(30));
                    try
                    {
                        await mail.SendAsync(message.Mail, timeout.Token);
                        await outbox.MarkDeliveredAsync(message.Id, stoppingToken);
                    }
                    catch (Exception exception) when (!stoppingToken.IsCancellationRequested)
                    {
                        // SMTP exception messages may contain customer data.
                        logger.LogWarning("Email {MessageId} delivery failed ({FailureType}); retry scheduled.", message.Id, exception.GetType().Name);
                        await outbox.ScheduleRetryAsync(message.Id, stoppingToken);
                    }
                    await unitOfWork.SaveChangesAsync(stoppingToken);
                    await transaction.CommitAsync(stoppingToken);
                    processed = true;
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
            catch (Exception exception)
            {
                logger.LogError("Email outbox processing failed ({FailureType}).", exception.GetType().Name);
            }

            if (!processed)
            {
                try { await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
            }
        }
    }
}
