using Schedulist.DAL.Models;

namespace Schedulist.DAL.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        List<Position> GetAllPositions();
        Position GetPositionById(int Id);
        Task<List<Position>> GetAllPositionsAsync();
    }
}
