using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tibber.TechnicalCase.Domain.Dtos;
using Tibber.TechnicalCase.Domain.Repositories;
using Tibber.TechnicalCase.Infrastructure.Entities;

namespace Tibber.TechnicalCase.Infrastructure.Repositories;

internal class ResultRepository : IResultRepository
{
    private readonly TibberDbContext _db;
    private readonly IMapper _mapper;

    public ResultRepository(TibberDbContext db, IMapper mapper)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ResultDto[]> ReadResults(int count = 50, int start = -1,
        CancellationToken cancellationToken = default)
    {
        var results = await _db.Results.Where(r => r.Id > start)
            .OrderBy(r => r.Id).Take(count)
            .ToArrayAsync(cancellationToken);

        return _mapper.Map<ResultDto[]>(results);
    }

    public async Task<ResultDto> AppendResultAsync(int commands, int result, double duration, DateTimeOffset time,
        CancellationToken cancellationToken = default)
    {
        var resultEntity = new ResultEntity() {
            TimeStamp = time, Commands = commands, Duration = duration, Result = result
        };

        await _db.Results.AddAsync(resultEntity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResultDto>(resultEntity);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _db.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        await _db.DisposeAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }
}
