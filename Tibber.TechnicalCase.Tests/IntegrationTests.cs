using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Tibber.TechnicalCase.Domain.Dtos;
using Tibber.TechnicalCase.Infrastructure;
using Tibber.TechnicalCase.Infrastructure.Extensions;

namespace Tibber.TechnicalCase.Tests;

public class IntegrationTests : IDisposable
{
    private SqliteConnection _connectionIdentity;
    private WebApplicationFactory<Program> _webApplication;
    private const string url = "/tibber-developer-test/enter-path";
    private bool disposedValue;

    public IntegrationTests()
    {
        _connectionIdentity = new SqliteConnection($"Data Source={Guid.NewGuid()};Mode=Memory;");
        _webApplication = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.Single(d => d.ServiceType == typeof(TibberDbContext));
                    services.Remove(descriptor);
                    
                    var dbcontenxtOption = services.Single(d => d.ServiceType == typeof(DbContextOptions<TibberDbContext>));
                    services.Remove(dbcontenxtOption);

                   
                    _connectionIdentity.Open();
                    services.AddDbContext<TibberDbContext>(options => options.UseSqlite(_connectionIdentity));

                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;

                    using var db = scopedServices.GetRequiredService<TibberDbContext>();
                    
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                });
            });
    }

    [Fact] public async Task TEST_ONE_COMMAND_NORTH_ASYNC()
    {
        Position start = new(1, 1);
        Command[] commands = new Command[] { new(Direction.North, 3) };
        
        RequestViewModel viewModel = new(start, commands);

        var cleint = _webApplication.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        var response = await cleint.PostAsJsonAsync(url, viewModel);

        Assert.NotNull(response);
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ResultDto>();
        Assert.NotNull(result);
        Assert.Equal(4, result.Result);
        Assert.Equal(1, result.Commands);
    }

    [Fact] public async Task TEST_ONE_COMMAND_SOUTH_ASYNC()
    {
        Position start = new(1, 1);
        Command[] commands = new Command[] { new(Direction.South, 3) };

        RequestViewModel viewModel = new(start, commands);

        var cleint = _webApplication.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        var response = await cleint.PostAsJsonAsync(url, viewModel);

        Assert.NotNull(response);
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ResultDto>();
        Assert.NotNull(result);
        Assert.Equal(4, result.Result);
        Assert.Equal(1, result.Commands);
    }

    [Fact] public async Task TEST_ONE_COMMAND_EAST_ASYNC()
    {
        Position start = new(1, 1);
        Command[] commands = new Command[] { new(Direction.East, 3) };

        RequestViewModel viewModel = new(start, commands);

        var cleint = _webApplication.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        var response = await cleint.PostAsJsonAsync(url, viewModel);

        Assert.NotNull(response);
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ResultDto>();
        Assert.NotNull(result);
        Assert.Equal(4, result.Result);
        Assert.Equal(1, result.Commands);
    }

    [Fact] public async Task TEST_ONE_COMMAND_WEST_ASYNC()
    {
        Position start = new(1, 1);
        Command[] commands = new Command[] { new(Direction.West, 3) };

        RequestViewModel viewModel = new(start, commands);

        var cleint = _webApplication.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        var response = await cleint.PostAsJsonAsync(url, viewModel);

        Assert.NotNull(response);
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ResultDto>();
        Assert.NotNull(result);
        Assert.Equal(4, result.Result);
        Assert.Equal(1, result.Commands);
    }

    [Fact] public async Task TEST_MULTIPLE_COMMAND_WITHOUT_OVERLAP_ASYNC()
    {
        Position start = new(1, 1);
        Command[] commands = new Command[] { 
            new(Direction.West, 3),
            new(Direction.North, 2)
        };

        RequestViewModel viewModel = new(start, commands);

        var cleint = _webApplication.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        var response = await cleint.PostAsJsonAsync(url, viewModel);

        Assert.NotNull(response);
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ResultDto>();
        Assert.NotNull(result);
        Assert.Equal(6, result.Result);
        Assert.Equal(2, result.Commands);
    }

    [Fact] public async Task TEST_MULTIPLE_COMMAND_WITH_OVERLAP_ASYNC()
    {
        Position start = new(1, 1);
        Command[] commands = new Command[] {
            new(Direction.West, 3),
            new(Direction.East, 4)
        };

        RequestViewModel viewModel = new(start, commands);

        var cleint = _webApplication.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        var response = await cleint.PostAsJsonAsync(url, viewModel);

        Assert.NotNull(response);
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ResultDto>();
        Assert.NotNull(result);
        Assert.Equal(5, result.Result);
        Assert.Equal(2, result.Commands);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            _connectionIdentity?.Dispose();
            _webApplication?.Dispose();
            disposedValue = true;
        }
    }
    
    ~IntegrationTests()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
