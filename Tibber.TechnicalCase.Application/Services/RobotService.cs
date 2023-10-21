using Tibber.TechnicalCase.Domain.Dtos;
using Tibber.TechnicalCase.Domain.Services;

namespace Tibber.TechnicalCase.Application.Services;

internal class RobotService : IRobotService
{
    private Position? _currentPosition;
    private ISet<Position>? _cleanedPositions;

    public void Initialize(Position start)
    {
        _currentPosition = start;
        _cleanedPositions = new HashSet<Position> { start };
    }

    public void Move(Command commands)
    {
        CheckInitialized();

        int count = _cleanedPositions!.Count;
        var positions = Enumerable.Range(1, commands.Steps)
            .Select(r => CalculatePosition(commands.Direction, r));
                
        _cleanedPositions.UnionWith(positions);
        _currentPosition = positions.Last();
    }

    public void Move(IEnumerable<Command> commands)
    { 
        foreach (var command in commands)
        {
            Move(command);
        }
    }


    public int UniqueCleanedPlaces()
    {
        CheckInitialized();
        return _cleanedPositions!.Count;
    }

    public Position[] UniqueCleanedPositions()
    {
        CheckInitialized();
        return _cleanedPositions!.ToArray();
    }

    private void CheckInitialized()
    {
        if (_currentPosition is null || _cleanedPositions is null)
        {
            throw new InvalidOperationException("Robot not initialized");
        }
    }

    private Position CalculatePosition(Direction direction, int step) => direction switch
    {
        Direction.North => _currentPosition! with { Y = _currentPosition.Y + step },
        Direction.South => _currentPosition! with { Y = _currentPosition.Y - step },
        Direction.East => _currentPosition! with { X = _currentPosition.X + step },
        Direction.West => _currentPosition! with { X = _currentPosition.X - step },
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
    };
}
