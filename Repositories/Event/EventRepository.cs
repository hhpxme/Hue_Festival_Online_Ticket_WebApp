using AutoMapper;
using HF_WEB_API.Data;
using HF_WEB_API.Models.Event;
using Microsoft.EntityFrameworkCore;

namespace HF_WEB_API.Repositories.Event
{
    public class EventRepository : IEventRepository
    {
        private readonly WebDbContext _context;
        private readonly IMapper _mapper;

        public EventRepository(WebDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Data.Event> AddEventAsync(EventModel model)
        {
            var ev = _mapper.Map<Data.Event>(model);
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            return ev;
        }

        public async Task DeleteEventAsync(int id)
        {
            var ev = _context.Events!.SingleOrDefault(p => p.Id == id);
            if (ev != null)
            {
                _context.Events!.Remove(ev);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<EventInformationModel>> GetAllEventAsync()
        {
            var ev = await _context.Events!.ToListAsync();
            return _mapper.Map<List<EventInformationModel>>(ev);
        }

        public async Task<EventInformationModel> GetEventAsync(int id)
        {
            var ev = await _context.Events!.FindAsync(id);
            return _mapper.Map<EventInformationModel>(ev);
        }

        public async Task UpdateEventAsync(int id, EventModel model)
        {
            var ev = _context.Events.FirstOrDefault(p => p.Id == id);
            if (ev != null)
            {
                ev = _mapper.Map<Data.Event>(model);
                _context.Events!.Update(ev);
                await _context.SaveChangesAsync();
            }
        }
    }
}
