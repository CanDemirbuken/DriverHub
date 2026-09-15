using DriverHub.Application.Contracts.Communication.Mail;
using DriverHub.Application.Interfaces.Communication;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Infrastructure.Services.Communication.Mail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace DriverHub.Tests;

public sealed class EmailOutboxWorkerTests
{
    [Theory]
    [InlineData(true)] [InlineData(false)]
    public async Task Worker_delivers_or_schedules_retry_using_abstract_mail_service(bool fail)
    {
        var fake = new FakeDelivery(fail);
        var services = new ServiceCollection();
        services.AddSingleton<IEmailOutbox>(fake);
        services.AddSingleton<IMailService>(fake);
        services.AddSingleton<IUnitOfWork>(fake);
        await using var provider = services.BuildServiceProvider();
        using var worker = new EmailOutboxWorker(provider.GetRequiredService<IServiceScopeFactory>(), NullLogger<EmailOutboxWorker>.Instance);
        await worker.StartAsync(default);
        await fake.Committed.Task.WaitAsync(TimeSpan.FromSeconds(5));
        await worker.StopAsync(default);
        Assert.Equal(fail, fake.Retried);
        Assert.Equal(!fail, fake.Delivered);
    }

    private sealed class FakeDelivery(bool fail) : IEmailOutbox, IMailService, IUnitOfWork, IUnitOfWorkTransaction
    {
        private bool claimed;
        public bool Retried { get; private set; }
        public bool Delivered { get; private set; }
        public TaskCompletionSource Committed { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public Task<PendingEmail?> GetNextForDeliveryAsync(CancellationToken cancellationToken)
        {
            if (claimed) return Task.FromResult<PendingEmail?>(null);
            claimed = true;
            return Task.FromResult<PendingEmail?>(new(Guid.NewGuid(), new("test@example.test", "Test", "Body")));
        }
        public Task SendAsync(SendMailRequest request, CancellationToken cancellationToken = default) => fail ? Task.FromException(new IOException("Simulated SMTP outage")) : Task.CompletedTask;
        public Task MarkDeliveredAsync(Guid id, CancellationToken cancellationToken) { Delivered = true; return Task.CompletedTask; }
        public Task ScheduleRetryAsync(Guid id, CancellationToken cancellationToken) { Retried = true; return Task.CompletedTask; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(1);
        public Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => Task.FromResult<IUnitOfWorkTransaction>(this);
        public Task CommitAsync(CancellationToken cancellationToken = default) { Committed.TrySetResult(); return Task.CompletedTask; }
        public Task RollbackAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
