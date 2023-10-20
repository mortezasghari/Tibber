using System.ComponentModel.DataAnnotations;

namespace Tibber.TechnicalCase.Domain.Dtos;

public record RequestViewModel(Position Start, Command[] Commands);
public record Command(Direction Direction, [Range(1, 200_000)]int Steps);
public record Position([Range(-100_000, 100_000)]int X, [Range(-100_000, 100_000)] int Y);
public record ResultDto(int Id, DateTimeOffset TimeStamp, int Commands, int Result, double Duration);

public enum Direction { North, East, South, West }
