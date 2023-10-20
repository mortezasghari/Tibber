using Tibber.TechnicalCase.Domain.Dtos;

namespace Tibber.TechnicalCase.Domain.Repositories;

public interface IResultRepository: IDisposable, IAsyncDisposable
{
    Task<ResultDto[]> ReadResults(int count = 50, int start = -1, CancellationToken cancellationToken = default);
    Task<ResultDto> AppendResultAsync(int commands, int result, DateTimeOffset time, CancellationToken cancellationToken = default);
}