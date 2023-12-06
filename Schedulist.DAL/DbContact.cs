using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public class DBContact : IdentityDbContext<IdentityUser>
    {
        public DBContact(DbContextOptions<DBContact> options) : base(options) { }
    }
}
