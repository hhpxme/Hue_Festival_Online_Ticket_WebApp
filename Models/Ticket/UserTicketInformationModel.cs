namespace HF_WEB_API.Models.Ticket
{
    public class UserTicketInformationModel
    {
        public string? BarCode { get; set; }

        public string? QRCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public string OwnerName { get; set; }

        public string OwnerId { get; set; }

        public DateTime OwnerDOB { get; set; }

        public int? EventId { get; set; }
    }
}
