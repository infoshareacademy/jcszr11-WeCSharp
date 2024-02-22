using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL.Repositories
{
    public class BaseRepository
    {
        protected SchedulistDbContext _db { get; set; }
        protected readonly ILogger<BaseRepository> _logger;
        public BaseRepository(SchedulistDbContext db, ILogger<BaseRepository> logger)
        {
            _logger = logger;
            _db = db;
        }
    }
}
