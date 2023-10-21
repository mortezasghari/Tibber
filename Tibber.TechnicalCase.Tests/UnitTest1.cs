using Tibber.TechnicalCase.Domain.Services;
using Tibber.TechnicalCase.Application.Services;
using Tibber.TechnicalCase.Domain.Dtos;

namespace Tibber.TechnicalCase.Tests;

public class UnitTest1
{
    IRobotService _robotService = new RobotService();

    [Fact]
    public void TEST_ONE_COMMAND_NORTH()
    {
        Position position = new(1, 1);
        _robotService.Initialize(position);

        Command command = new Command(Direction.North, 2);
        _robotService.Move(command);

        var result = _robotService.UniqueCleanedPlaces();
        Assert.Equal(3, result);

        var cleaned = _robotService.UniqueCleanedPositions();
        Assert.Equal(3, cleaned.Length);
        
        Assert.Equal(position, cleaned[0]);
        Assert.Equal(position with { Y = position.Y + 1 }, cleaned[1]);
        Assert.Equal(position with { Y = position.Y + 2 }, cleaned[2]);
    }

    [Fact]
    public void TEST_ONE_COMMAND_SOUTH()
    {
        Position position = new(1, 1);
        _robotService.Initialize(position);

        Command command = new Command(Direction.South, 2);
        _robotService.Move(command);

        var result = _robotService.UniqueCleanedPlaces();
        Assert.Equal(3, result);

        var cleaned = _robotService.UniqueCleanedPositions();
        Assert.Equal(3, cleaned.Length);

        Assert.Equal(position, cleaned[0]);
        Assert.Equal(position with { Y = position.Y - 1 }, cleaned[1]);
        Assert.Equal(position with { Y = position.Y - 2 }, cleaned[2]);
    }

    [Fact]
    public void TEST_ONE_COMMAND_EAST()
    {
        Position position = new(1, 1);
        _robotService.Initialize(position);

        Command command = new Command(Direction.East, 2);
        _robotService.Move(command);

        var result = _robotService.UniqueCleanedPlaces();
        Assert.Equal(3, result);

        var cleaned = _robotService.UniqueCleanedPositions();
        Assert.Equal(3, cleaned.Length);

        Assert.Equal(position, cleaned[0]);
        Assert.Equal(position with { X = position.X + 1 }, cleaned[1]);
        Assert.Equal(position with { X = position.X + 2 }, cleaned[2]);
    }

    [Fact]
    public void TEST_ONE_COMMAND_WEST()
    {
        Position position = new(1, 1);
        _robotService.Initialize(position);

        Command command = new Command(Direction.West, 2);
        _robotService.Move(command);

        var result = _robotService.UniqueCleanedPlaces();
        Assert.Equal(3, result);

        var cleaned = _robotService.UniqueCleanedPositions();
        Assert.Equal(3, cleaned.Length);

        Assert.Equal(position, cleaned[0]);
        Assert.Equal(position with { X = position.X - 1 }, cleaned[1]);
        Assert.Equal(position with { X = position.X - 2 }, cleaned[2]);
    }


    [Fact]
    public void TEST_MULTIPLE_COMMAND_WITHOUT_OVERLAP()
    {
        Position position = new(1, 1);
        _robotService.Initialize(position);

        Command[] commands = new Command[]
        {
            new Command(Direction.North, 3), 
            new Command(Direction.East, 2),
        };

        _robotService.Move(commands);
        var result = _robotService.UniqueCleanedPlaces();

        Assert.Equal(6, result);

        var cleaned = _robotService.UniqueCleanedPositions();
        Assert.Equal(6, cleaned.Length);

        Assert.Equal(position, cleaned[0]);
        Assert.Equal(position with { Y = position.Y + 1 }, cleaned[1]);
        Assert.Equal(position with { Y = position.Y + 2 }, cleaned[2]);
        Assert.Equal(position with { Y = position.Y + 3 }, cleaned[3]);
        Assert.Equal(position with { X = position.X + 1, Y = position.Y + 3 }, cleaned[4]);
        Assert.Equal(position with { X = position.X + 2, Y = position.Y + 3 }, cleaned[5]);
    }

    [Fact]
    public void TEST_MULTIPLE_COMMAND_WITH_OVERLAP()
    {
        Position position = new(1, 1);
        _robotService.Initialize(position);

        Command[] commands = new Command[]
        {
            new Command(Direction.North, 3),
            new Command(Direction.South, 2),
        };
                
        _robotService.Move(commands);
        var result = _robotService.UniqueCleanedPlaces();

        Assert.Equal(4, result);

        var cleaned = _robotService.UniqueCleanedPositions();
        Assert.Equal(4, cleaned.Length);

        Assert.Equal(position, cleaned[0]);
        Assert.Equal(position with { Y = position.Y + 1 }, cleaned[1]);
        Assert.Equal(position with { Y = position.Y + 2 }, cleaned[2]);
        Assert.Equal(position with { Y = position.Y + 3 }, cleaned[3]);
    }
}