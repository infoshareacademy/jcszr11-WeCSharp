using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.DAL.Repositories
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {
        private readonly UserManager<User> _userManager;
        public DepartmentRepository(SchedulistDbContext db, ILogger<BaseRepository> logger, UserManager<User> userManager) : base(db, logger)
        {
            _userManager = userManager;
        }
        public List<Department> GetAllDepartments()
        {
            try
            {
                return _db.Departments.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Departments from the database.");
                return new List<Department>();
            }
        }
        public Department GetDepartmentById(int Id)
        {
            try
            {
                return _db.Departments.FirstOrDefault(d => d.Id == Id) ?? throw new Exception("Department not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Departments from the database.");
                return new Department();
            }
        }
    }
}
