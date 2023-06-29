using System.Security.Claims;

namespace HF_WEB_API.Helper.UserServices
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (identity != null)
            {
                var userClaims = identity.Claims;

                var sid = userClaims.FirstOrDefault(p => p.Type == ClaimTypes.Sid)?.Value;
#pragma warning disable CS8603 // Possible null reference return.
                return sid;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetUserName()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (identity != null)
            {
                var username = identity.Name?.ToString();

#pragma warning disable CS8603 // Possible null reference return.
                return username;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
