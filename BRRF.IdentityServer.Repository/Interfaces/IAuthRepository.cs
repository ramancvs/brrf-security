using BRRF.IdentityServer.Repository.Entities;

namespace BRRF.IdentityServer.Repository
{
    public interface IAuthRepository
    {
        User GetUserById(int id);
        User GetUserByUsername(string username);
        bool ValidatePassword(string username, string plainTextPassword);
    }
}
