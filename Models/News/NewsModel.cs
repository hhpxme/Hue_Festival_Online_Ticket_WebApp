using System.ComponentModel.DataAnnotations;

namespace HF_WEB_API.Models.News
{
    public class NewsModel
    {
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string NewsContent { get; set; } = string.Empty;

        public string? AccountId { get; set; }
    }
}
