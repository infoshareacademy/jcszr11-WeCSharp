using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.DAL.Repositories
{
    public class PositionRepository : BaseRepository, IPositionRepository
    {
        private readonly UserManager<User> _userManager;
        public PositionRepository(SchedulistDbContext db, ILogger<BaseRepository> logger, UserManager<User> userManager) : base(db, logger)
        {
            _userManager = userManager;
        }
        public List<Position> GetAllPositions()
        {
            try
            {
                return _db.Positions.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Positions from the database.");
                return new List<Position>();
            }
        }
        public Position GetPositionById(int Id)
        {
            try
            {
                return _db.Positions.FirstOrDefault(d => d.Id == Id) ?? throw new Exception("Position not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Departments from the database.");
                return new Position();
            }
        }
        public async Task<List<Position>> GetAllPositionsAsync()
        {
            try
            {
                return await _db.Positions.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Departments from the database.");
                return new List<Position>();
            }
        }
    }
}