using System.ComponentModel.DataAnnotations;

namespace HF_WEB_API.Data
{
    public class Ticket
    {
        [Key]
        [Required]
        public string Id { get; set; }

        public string? BarCode { get; set; }

        public string? QRCode { get; set; }

        public DateTime CreatedDate { get; set; }

        // Ticket is Active ??
        [Required]
        public bool IsActive { get; set; }

        // Ticket is Purchased ??
        public bool? IsPay { get; set; }

        // Ticket's Owner Info
        [Required]
        public string OwnerName { get; set; }

        [Required]
        public string OwnerId { get; set; } //This CitizenId of User //Căn cước công dân

        [Required]
        public DateTime OwnerDOB { get; set; }

        //Foreign Key
        [Required]
        [StringLength(450)]
        public string? AccountId { get; set; }
        public virtual Account? Account { get; set; }

        [Required]
        public int? EventId { get; set; }
        public virtual Event? Event { get; set; }
    }
}
