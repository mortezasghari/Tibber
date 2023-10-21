using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibber.TechnicalCase.Infrastructure.Extensions;

internal class TibberDbContextFactory : IDesignTimeDbContextFactory<TibberDbContext>
{
    public TibberDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TibberDbContext>();
        optionsBuilder.UseNpgsql("Data Source=blog.db");

        return new TibberDbContext(optionsBuilder.Options);
    }
}
