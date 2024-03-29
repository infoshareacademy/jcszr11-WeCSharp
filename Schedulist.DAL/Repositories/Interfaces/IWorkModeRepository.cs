﻿using Schedulist.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL.Repositories.Interfaces
{
    public interface IWorkModeRepository
    {
        public List<WorkMode> GetAllWorkModes();
        public WorkMode GetWorkModeById(int id);
    }
}
