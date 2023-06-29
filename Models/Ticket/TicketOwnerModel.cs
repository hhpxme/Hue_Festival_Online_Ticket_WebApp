using System.ComponentModel.DataAnnotations;

namespace HF_WEB_API.Models.Ticket
{
    public class TicketOwnerModel
    {
        public string OwnerName { get; set; }

        public string OwnerId { get; set; }

        public DateTime OwnerDOB { get; set; }
    }
}
