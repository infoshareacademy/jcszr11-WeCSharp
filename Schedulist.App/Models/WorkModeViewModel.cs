using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.DAL;

namespace Schedulist.App.Models
{
    public class WorkModeViewModel
    {
        public DateOnly Date { get; set; }
        public string WorkModeName { get; set; }
        public int UserID { get; set; }
        [BindProperty]
        public List<SelectListItem> GetAllWorkModeNames { get;set; }
        

             

        
    }
    
}
