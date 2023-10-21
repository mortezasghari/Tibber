using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tibber.TechnicalCase.Domain.Repositories;
using Tibber.TechnicalCase.Infrastructure.Mappers;
using Tibber.TechnicalCase.Infrastructure.Repositories;

namespace Tibber.TechnicalCase.Infrastructure.Extensions;

public static class StartUpExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var coonectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TibberDbContext>(options => options.UseNpgsql(coonectionString));

        services.AddHostedService<CheckMigerationService>();
        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddAutoMapper(typeof(ResultMapperProfile).Assembly);
    }
}
