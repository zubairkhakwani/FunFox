using FunFox.Business.Enums;

namespace FunFox.Business.Services.Contracts
{
    public interface IIdentityUser
    {
        bool IsAuthenticated { get; }
        int Id { get;}
        string Name { get; }
        string Email { get;}
        string Role { get; }
        ClassLevel? Level { get; }

        bool IsInRole(string role);
    }
}
