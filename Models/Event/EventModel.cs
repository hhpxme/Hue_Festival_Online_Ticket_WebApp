using System.ComponentModel.DataAnnotations;

namespace HF_WEB_API.Models.Event
{
    public class EventModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string Location { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TotalNumOfTicket { get; set; }

        public int NumOfTicketsSold { get; set; }

        public long TicketPrice { get; set; }
    }
}
