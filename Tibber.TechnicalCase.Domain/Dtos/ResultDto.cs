namespace Tibber.TechnicalCase.Domain.Dtos;

public record ResultDto(int Id, DateTimeOffset TimeStamp, int Commands, int Result, double Duration);