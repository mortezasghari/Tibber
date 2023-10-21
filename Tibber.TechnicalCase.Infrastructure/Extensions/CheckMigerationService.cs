using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tibber.TechnicalCase.Infrastructure.Extensions;

internal class CheckMigerationService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public CheckMigerationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {

        await using var scope = _serviceProvider.CreateAsyncScope();
        await using var db = scope.ServiceProvider.GetRequiredService<TibberDbContext>();
                
        var pendding = await db.Database.GetPendingMigrationsAsync(cancellationToken);

        if (pendding.Any()) 
            await db.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
