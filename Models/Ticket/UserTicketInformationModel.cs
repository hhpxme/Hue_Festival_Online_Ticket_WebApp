namespace HF_WEB_API.Models.Ticket
{
    public class UserTicketInformationModel
    {
        public DateTime CreatedDate { get; set; }

        public string OwnerName { get; set; }

        public string OwnerId { get; set; }

        public DateTime OwnerDOB { get; set; }

        public bool IsActive { get; set; }

        public int? EventId { get; set; }
    }
}
