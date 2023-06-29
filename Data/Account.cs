using Microsoft.AspNetCore.Identity;

namespace HF_WEB_API.Data
{
    public class Account : IdentityUser
    {
        public string? FullName { get; set; }

        // For Refresh Token
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public virtual IEnumerable<News> Newses { get; set; } = Enumerable.Empty<News>();
        public virtual IEnumerable<Ticket> Tickets { get; set; } = Enumerable.Empty<Ticket>();
    }
}
