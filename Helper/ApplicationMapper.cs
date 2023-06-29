using AutoMapper;
using HF_WEB_API.Models.News;
using HF_WEB_API.Models.Event;
using HF_WEB_API.Data;
using HF_WEB_API.Models.Ticket;

namespace HF_WEB_API.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<News, NewsModel>().ReverseMap();
            CreateMap<Event, EventInformationModel>().ReverseMap();
            CreateMap<Event, EventModel>().ReverseMap();
            CreateMap<Ticket, AllInformationTicketModel>().ReverseMap();
            CreateMap<Ticket, TicketOwnerModel>().ReverseMap();
            CreateMap<Ticket, UserTicketInformationModel>().ReverseMap();
        }

    }
}
