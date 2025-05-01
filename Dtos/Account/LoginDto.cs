using System.ComponentModel.DataAnnotations;

namespace Server.Dto.Account;

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    public string Password { get; set; }
}