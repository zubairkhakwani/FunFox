using FunFox.Business.Enums;
using FunFox.Business.Services.Contracts;
using System.Security.Claims;

namespace FunFox.Services
{
    public class IdentityUser : IIdentityUser
    {
        public IdentityUser(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
            {
                IsAuthenticated = false;
                return;
            }

            var idClaim = httpContext.User.FindFirst("Id");
            var nameClaim = httpContext.User.FindFirst(ClaimTypes.Name);
            var emailClaim = httpContext.User.FindFirst(ClaimTypes.Email);
            var roleClaim = httpContext.User.FindFirst(ClaimTypes.Role);
            var levelClaim = httpContext.User.FindFirst("Level");

            IsAuthenticated = true;
            Id = Convert.ToInt32(idClaim?.Value);
            Name = nameClaim?.Value;
            Email = emailClaim?.Value;
            Role = roleClaim?.Value;

            var success = Enum.TryParse(levelClaim?.Value, true, out ClassLevel level);

            if (success)
            {
                Level = level;
            }
        }

        public bool IsAuthenticated { get; private set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public ClassLevel? Level { get; private set; }

        public bool IsInRole(string role)
        {
            return Role == role;
        }
    }
}
