using Microsoft.Extensions.Options;
using Schedulist.DAL.Models;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL.Sieve
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<CalendarEvent>(p => p.CalendarEventDate)
                .CanFilter()
                .CanSort();

            mapper.Property<CalendarEvent>(p => p.CalendarEventName)
                .CanFilter()
                .CanSort();
            
            mapper.Property<CalendarEvent>(p => p.CalendarEventStartTime)
                .CanSort();
            
            mapper.Property<CalendarEvent>(p => p.CalendarEventEndTime)
                .CanSort();

            mapper.Property<CalendarEvent>(p => p.UserId)
                .CanFilter()
                .CanSort();

            return mapper;
        }
    }
}
