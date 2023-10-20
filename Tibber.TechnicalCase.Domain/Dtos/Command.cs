using System.ComponentModel.DataAnnotations;

namespace Tibber.TechnicalCase.Domain.Dtos;

public record Command(Direction Direction, [Range(1, 200_000)]int Steps);
