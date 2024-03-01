using AutoMapper;
using Schedulist.DAL.Models;

namespace Schedulist.App
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        { 
            CreateMap<CalendarEvent, CalendarEventDto>();
        }
    }
}
