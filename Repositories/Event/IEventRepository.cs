using HF_WEB_API.Models.Event;
using HF_WEB_API.Models.News;

namespace HF_WEB_API.Repositories.Event
{
    public interface IEventRepository
    {
        public Task<List<EventInformationModel>> GetAllEventAsync();
        public Task<List<EventModel>> GetAllEventModelAsync();
        public Task<List<EventInformationModel>> GetAllEventByDateAsync(DateTime dateTime);
        public Task<EventInformationModel> GetEventAsync(int id);
        public Task<Data.Event> AddEventAsync(EventModel model);
        public Task UpdateEventAsync(int id, EventModel model);
        public Task UpdateEventCurrentTicketAsync(int id);
        public Task DeleteEventAsync(int id);
    }
}
