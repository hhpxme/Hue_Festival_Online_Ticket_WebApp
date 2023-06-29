using AutoMapper;
using HF_WEB_API.Models.Event;
using HF_WEB_API.Models.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace HF_WEB_API.Repositories.Ticket
{
    public class TicketRepository : ITicketRepository
    {
        private readonly WebDbContext _context;
        private readonly IMapper _mapper;

        public TicketRepository(WebDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ActiveTicketAsync(string id)
        {
            var ticket = _context.Tickets.FirstOrDefault(p => p.Id == id);
            if (ticket != null)
            {
                ticket.IsActive = true;
                _context.Tickets!.Update(ticket);
                await _context.SaveChangesAsync();
            }
        }

        public async Task PayTicketAsync(string id)
        {
            var ticket = _context.Tickets.FirstOrDefault(p => p.Id == id);
            if (ticket != null)
            {
                ticket.IsPay = true;
                _context.Tickets!.Update(ticket);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Data.Ticket> CreateTicketAsync(string id, string userId, int eventId, DateTime createTime, string barcodeLink, string qrcodeLink, TicketOwnerModel model)
        {
            var ticket = new Data.Ticket()
            {
                Id = id,
                AccountId = userId,
                EventId = eventId,

                CreatedDate = createTime,
                IsActive = false,
                IsPay = false,
                BarCode = barcodeLink,
                QRCode = qrcodeLink,

                OwnerName = model.OwnerName,
                OwnerId = model.OwnerId,
                OwnerDOB = model.OwnerDOB,
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        public async Task DeleteTicketAsync(string id)
        {
            var ticket = _context.Tickets!.SingleOrDefault(p => p.Id == id);
            if (ticket != null)
            {
                _context.Tickets!.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<AllInformationTicketModel>> GetAllTicketAsync()
        {
            var tickets = await _context.Tickets!.ToListAsync();
            return _mapper.Map<List<AllInformationTicketModel>>(tickets);
        }

        public async Task<List<AllInformationTicketModel>> GetAllTicketOfEventAsync(int eventId)
        {
            var tickets = await _context.Tickets.Where(p => p.EventId == eventId).ToListAsync();

            return _mapper.Map<List<AllInformationTicketModel>>(tickets);
        }

        public async Task<List<AllInformationTicketModel>> GetAllTicketOfUserAsync(string userId)
        {
            var tickets = await _context.Tickets!.Where(p => p.AccountId == userId).ToListAsync();
            return _mapper.Map<List<AllInformationTicketModel>>(tickets);
        }

        public async Task<List<UserTicketInformationModel>> GetAllTicketForUserAsync(string userId, bool isPay)
        {
            var tickets = await _context.Tickets!.Where(p => p.AccountId == userId && p.IsPay == isPay).ToListAsync();
            return _mapper.Map<List<UserTicketInformationModel>>(tickets);
        }

        public async Task<UserTicketInformationModel> GetTicketByIdAsync(string id)
        {
            var ticket = await _context.Tickets!.FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<UserTicketInformationModel>(ticket);
        }
    }
}
