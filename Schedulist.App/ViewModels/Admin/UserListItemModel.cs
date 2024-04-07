using Schedulist.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.App.ViewModels.Admin
{
    public class UserListItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Department Department { get; set; } = new Department();
        public Position Position { get; set; } = new Position();
        public string? Roles { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string? Email { get; set; }
    }
}
