using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tibber.TechnicalCase.Domain.Repositories;
using Tibber.TechnicalCase.Infrastructure.Repositories;

namespace Tibber.TechnicalCase.Infrastructure.Extensions;

public static class StartUpExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<TibberDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddAutoMapper(typeof(StartUpExtensions).Assembly);        
    }
}
