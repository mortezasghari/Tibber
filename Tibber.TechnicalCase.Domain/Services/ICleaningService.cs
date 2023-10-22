using System.ComponentModel.DataAnnotations;
using Tibber.TechnicalCase.Domain.Dtos;

namespace Tibber.TechnicalCase.Domain.Services;

public interface ICleaningService
{
    Task<ResultDto> RequestCleaning(RequestViewModel viewModel, CancellationToken cancellationToken = default);
}
