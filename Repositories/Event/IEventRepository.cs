using HF_WEB_API.Models.Event;

namespace HF_WEB_API.Repositories.Event
{
    public interface IEventRepository
    {
        public Task<List<EventInformationModel>> GetAllEventAsync();
        public Task<EventInformationModel> GetEventAsync(int id);
        public Task<Data.Event> AddEventAsync(EventModel model);
        public Task UpdateEventAsync(int id, EventModel model);
        public Task DeleteEventAsync(int id);
    }
}
