using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HF_WEB_API.Data
{
    public class News
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required]
        public string NewsContent { get; set; } = string.Empty;

        // Foreign Key
        [StringLength(450)]
        public string? AccountId { get; set; }
        public virtual Account? Account { get; set; }
    }
}
