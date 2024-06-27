using Microsoft.AspNetCore.Identity;

namespace CodePluse.API.Repository.Interface
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
