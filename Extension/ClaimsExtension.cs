using System.Security.Claims;

namespace Server.Extension;

public static class ClaimsExtension
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.Claims
            .SingleOrDefault(x => x.Type.Equals("https://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))
            .Value;
    }
    
}