using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    internal interface IWorkModesRepository
    {
        List<WorkModes> GetAllWorkModes();
        void AddWorkModes(WorkModes workMode);
    }
}
