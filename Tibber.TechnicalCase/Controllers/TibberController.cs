using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Tibber.TechnicalCase.Domain.Dtos;
using Tibber.TechnicalCase.Domain.Repositories;
using Tibber.TechnicalCase.Domain.Services;

namespace Tibber.TechnicalCase.Controllers;

[Route("/tibber-developer-test/enter-path"), ApiController, Consumes("application/json"), Produces("application/json") ]
public class TibberController : ControllerBase
{
    private readonly IRobotService _robotService;
    private readonly IResultRepository _repository;

    public TibberController(IRobotService robotService, IResultRepository repository)
    {
        _robotService = robotService;
        _repository = repository;
    }


    [HttpPost]
    public async Task<ActionResult<ResultDto>> PostCommand([Required, FromBody] RequestViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var time = DateTimeOffset.UtcNow;
        _robotService.Initialize(viewModel.Start);

        _robotService.Move(viewModel.Commands);
        var count = _robotService.UniqueCleanedPlaces();
        double duration = (DateTimeOffset.UtcNow - time).TotalSeconds;

        var result = await _repository.AppendResultAsync(viewModel.Commands.Length, count, duration, time, cancellationToken);
        return Ok(result);
    }
}
