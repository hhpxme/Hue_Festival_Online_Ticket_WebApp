using System.ComponentModel.DataAnnotations;

namespace HF_WEB_API.Models.Ticket
{
    public class AllInformationTicketModel
    {
        public string Id { get; set; }

        public string? BarCode { get; set; }

        public string? QRCode { get; set; }

        public DateTime CreatedDate { get; set; }

        // Ticket is Active ??
        public bool IsActive { get; set; }

        // Ticket is Purchased ??
        public bool IsPay { get; set; }

        // Ticket's Owner Info
        public string OwnerName { get; set; }

        public string OwnerId { get; set; }

        public DateTime OwnerDOB { get; set; }

        //Foreign Key
        public string? AccountId { get; set; }

        public int? EventId { get; set; }
    }
}
