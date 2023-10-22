using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Tibber.TechnicalCase.Domain.Dtos;
using Tibber.TechnicalCase.Domain.Repositories;
using Tibber.TechnicalCase.Domain.Services;

namespace Tibber.TechnicalCase.Controllers;

[Route("/tibber-developer-test/enter-path"), ApiController, Consumes("application/json"), Produces("application/json") ]
public class TibberController : ControllerBase
{
    private readonly ICleaningService _requestCleaning;

    public TibberController(ICleaningService requestCleaning)
    {
        _requestCleaning = requestCleaning;
    }

    [HttpPost]
    public async Task<ActionResult<ResultDto>> PostCommand([Required, FromBody] RequestViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var result = await _requestCleaning.RequestCleaning(viewModel, cancellationToken);
        return Ok(result);
    }
}
