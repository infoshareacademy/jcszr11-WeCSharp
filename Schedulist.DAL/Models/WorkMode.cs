﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL.Models
{
    public class WorkMode
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
