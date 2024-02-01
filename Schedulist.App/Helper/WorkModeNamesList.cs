using Schedulist.App.Models.Domain_Models;

namespace Schedulist.App.Helper
{
    public static class WorkModeNamesList
    {
        public static List<WorkModeName> GetAll() 
        {
            return new List<WorkModeName> 
            {
                new WorkModeName() {Name = "Office" ,   Id=1},
                new WorkModeName() {Name = "Home office",   Id=2},
                new WorkModeName() {Name = "Sick leave",    Id=3},
                new WorkModeName() {Name = "Holiday leave", Id=4},
                new WorkModeName() {Name = "Another work mode", Id=5}
            };
        }
    }
}
