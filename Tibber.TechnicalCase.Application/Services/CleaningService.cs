using Tibber.TechnicalCase.Domain.Dtos;
using Tibber.TechnicalCase.Domain.Repositories;
using Tibber.TechnicalCase.Domain.Services;

namespace Tibber.TechnicalCase.Application.Services;

internal class CleaningService : ICleaningService
{
    private readonly IRobotService _robotService;
    private readonly IResultRepository _resultRepository;

    public CleaningService(IRobotService robotService, IResultRepository resultRepository)
    {
        _robotService = robotService;
        _resultRepository = resultRepository;
    }
    
    public Task<ResultDto> RequestCleaning(RequestViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var time = DateTimeOffset.UtcNow;
        _robotService.Initialize(viewModel.Start);

        _robotService.Move(viewModel.Commands);
        var count = _robotService.UniqueCleanedPlaces();
        double duration = (DateTimeOffset.UtcNow - time).TotalSeconds;

        return _resultRepository.AppendResultAsync(viewModel.Commands.Length, count, duration, time, cancellationToken);
    }
}
