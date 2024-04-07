using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Schedulist.DAL.Models;

namespace Schedulist.App.ViewModels.Admin
{
    public class AdminViewModel
    {
        public List<UserListItemModel>? Users { get; set; }
        public List<WorkMode>? ListOfWorkModes { get; set; } = new List<WorkMode>();

        [ValidateNever]
        public WorkMode? WorkMode { get; set; } = new WorkMode();
        public User? User { get; set; } = new User();
        [ValidateNever]
        public Department? Department { get; set; } = new Department();
        public List<Department> Departments { get; set; } = new List<Department>();
        [ValidateNever]
        public Position? Position { get; set; } = new Position();
        public List<Position> Positions { get; set; } = new List<Position>();

    }
}
