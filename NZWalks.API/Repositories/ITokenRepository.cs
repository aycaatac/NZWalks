using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        //why dont we have a repository for authentication??

        public string CreateJWTToken(IdentityUser user, List<string> Roles);
    }
}
