using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HF_WEB_API.Data
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        // Event Information
        [Required]
        [StringLength(256)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [StringLength(3999)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        // Ticket Information
        public int TotalNumOfTicket { get; set; } = 0;

        public int NumOfTicketsSold { get; set; } = 0;

        [Required]
        [Column(TypeName = "bigint")]
        public long TicketPrice { get; set; }

        // Ticket List of Events
        public virtual IEnumerable<Ticket> Tickets { get; set; } = Enumerable.Empty<Ticket>();

    }
}
