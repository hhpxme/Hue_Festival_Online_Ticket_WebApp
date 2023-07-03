using HF_WEB_API.Models.Event;

namespace HF_WEB_API.Repositories.Event
{
    public interface IEventRepository
    {
        public Task<List<EventInformationModel>> GetAllEventAsync();
        public Task<List<EventModel>> GetAllEventModelAsync();
        public Task<EventInformationModel> GetEventAsync(int id);
        public Task<Data.Event> AddEventAsync(EventModel model);
        public Task UpdateEventAsync(int id, EventModel model);
        public Task UpdateEventCurrentTicketAsync(int id, int quantity);
        public Task DeleteEventAsync(int id);
    }
}
