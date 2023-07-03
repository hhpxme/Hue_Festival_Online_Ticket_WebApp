using HF_WEB_API.Models.Event;
using HF_WEB_API.Models.Ticket;

namespace HF_WEB_API.Repositories.Ticket
{
    public interface ITicketRepository
    {
        public Task<List<AllInformationTicketModel>> GetAllTicketAsync();
        public Task<List<AllInformationTicketModel>> GetAllTicketOfEventAsync(int eventId);
        public Task<List<AllInformationTicketModel>> GetAllTicketOfUserAsync(string userId);
        public Task<List<UserTicketInformationModel>> GetAllTicketForUserAsync(string userId, bool isPay);
        public Task<UserTicketInformationModel> GetTicketByIdAsync(string id);
        public Task<Data.Ticket> CreateTicketAsync(string id, string userId, int eventId, DateTime createTime, string barcodeLink, string qrcodeLink, TicketOwnerModel model);
        public Task ActiveTicketAsync(string id);
        public Task PayTicketAsync(string id);
        public Task DeleteTicketAsync(string id);
    }
}
