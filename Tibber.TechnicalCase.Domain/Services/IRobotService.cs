using Tibber.TechnicalCase.Domain.Dtos;

namespace Tibber.TechnicalCase.Domain.Services;

public interface IRobotService
{
    void Move(Command command);
    void Move(IEnumerable<Command> commands);

    void Initialize(Position start);

    int UniqueCleanedPlaces();
    Position[] UniqueCleanedPositions();
}
