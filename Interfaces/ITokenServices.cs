using API.Models;

namespace Server.Interfaces;

public interface ITokenServices
{
    string CreateToken(AppUser user);
}