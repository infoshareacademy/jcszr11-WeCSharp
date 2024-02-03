using Microsoft.Extensions.Logging;
using Schedulist.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL.Repositories
{
    public class CalendarRepository : BaseRepository, ICalendarRepository
    {

        public CalendarRepository(DBContact db, ILogger<BaseRepository> logger) : base(db, logger)
        {

        }
    }
}
