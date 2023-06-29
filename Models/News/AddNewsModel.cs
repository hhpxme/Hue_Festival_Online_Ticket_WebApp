using System.ComponentModel.DataAnnotations;

namespace HF_WEB_API.Models.News
{
    public class AddNewsModel
    {
        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        public string NewsContent { get; set; } = string.Empty;
    }
}
