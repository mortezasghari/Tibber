using Tibber.TechnicalCase.Domain.Dtos;

namespace Tibber.TechnicalCase.Domain.Services;

public interface IRobotService
{
    int Move(Command commands);
    void Initialize(Position start);

    int UniqueCleanedPlaces();
    Position[] UniqueCleanedPositions();
}
