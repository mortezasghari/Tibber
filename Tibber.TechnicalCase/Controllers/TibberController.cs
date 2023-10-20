using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tibber.TechnicalCase.Domain.Dtos;

namespace Tibber.TechnicalCase.Controllers;

[Route("/tibber-developer-test/enter-path"), ApiController]
public class TibberController : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<ResultDto>> PostCommand([Required, FromBody] RequestViewModel viewModel)
    {
        throw new NotImplementedException();
    }
}