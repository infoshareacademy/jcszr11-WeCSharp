using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    internal class WorkModes
    {
        public int WorkModeID { get; set; }
        public string WorkModeName { get; set; }
        
        public WorkModes(int idWM, string nameWM)
        {
            WorkModeID = idWM;
            WorkModeName = nameWM;
        }
    }
}
