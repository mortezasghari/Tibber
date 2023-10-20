using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Tibber.TechnicalCase.Domain.Dtos;
using Tibber.TechnicalCase.Domain.Repositories;
using Tibber.TechnicalCase.Domain.Services;

namespace Tibber.TechnicalCase.Controllers;

[Route("/tibber-developer-test/enter-path"), ApiController]
public class TibberController : ControllerBase
{
    private readonly IRobotService _robotService;
    //private readonly IResultRepository _repository;

    public TibberController(IRobotService robotService)
    {
        _robotService = robotService;
        //_repository = repository;
    }


    [HttpPost]
    public async Task<ActionResult<ResultDto>> PostCommand([Required, FromBody] RequestViewModel viewModel, CancellationToken cancellationToken = default)
    {
        int count = 1;
        var time = DateTimeOffset.UtcNow;

        _robotService.Initialize(viewModel.Start);
        foreach (var command in viewModel.Commands)
        {
            count += _robotService.Move(command);
        }

        //var result = await _repository.AppendResultAsync(viewModel.Commands.Length, count, time, cancellationToken);
        return Ok(count);
    }
}