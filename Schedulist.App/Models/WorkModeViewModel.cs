using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.DAL;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.App.Models
{
    public class WorkModeViewModel
    {
        public DateOnly Date { get; set; }
        public string WorkModeName { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Select Work Mode")]
        public int SelectedWorkModeId { get; set; }

        [BindProperty]
        public List<SelectListItem> GetAllWorkModeNames { get;set; }


        

             

        
    }
    
}
