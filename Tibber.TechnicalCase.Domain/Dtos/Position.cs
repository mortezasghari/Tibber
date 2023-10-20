using System.ComponentModel.DataAnnotations;

namespace Tibber.TechnicalCase.Domain.Dtos;

public record Position([Range(-100_000, 100_000)]int X, [Range(-100_000, 100_000)] int Y);
