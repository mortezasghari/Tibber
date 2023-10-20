using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Tibber.TechnicalCase.Infrastructure.Entities;

namespace Tibber.TechnicalCase.Infrastructure;

internal class TibberDbContext : DbContext
{
    public DbSet<ResultEntity> Results => Set<ResultEntity>();

    public TibberDbContext([NotNull] DbContextOptions options) : base(options)
    {
    }
}