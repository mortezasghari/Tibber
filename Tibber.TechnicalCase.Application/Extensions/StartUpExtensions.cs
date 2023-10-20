using Microsoft.Extensions.DependencyInjection;
using Tibber.TechnicalCase.Application.Services;
using Tibber.TechnicalCase.Domain.Services;

namespace Tibber.TechnicalCase.Application.Extensions;

public static class StartUpExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRobotService, RobotService>();
    }
}
