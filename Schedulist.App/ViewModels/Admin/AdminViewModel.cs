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

    }
}
