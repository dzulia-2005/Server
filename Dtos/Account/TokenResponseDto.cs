using System.ComponentModel.DataAnnotations;

namespace Server.Dtos.Account;

public class TokenResponseDto
{
    [Required]
    public string AccessToken { get; set; }
    [Required]
    public string RefreshToken { get; set; }
}