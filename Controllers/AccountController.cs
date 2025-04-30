using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Dto.Account;
using Server.Dtos.Account;
using Server.Interfaces;

namespace Server.Controllers;
[Route("api/account")]
[ApiController]
public class AccountController:ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenServices _tokenServices;

    public AccountController(UserManager<AppUser> userManager,ITokenServices tokenServices)
    {
        _userManager = userManager;
        _tokenServices = tokenServices;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };
            var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (createUser.Succeeded)
            {
                var roles = await _userManager.AddToRoleAsync(appUser,"User");
                if (roles.Succeeded)
                {
                    return Ok
                    (
                        new NewUserDto
                        {
                            Email = registerDto.Email,
                            UserName = registerDto.Username,
                            Token = _tokenServices.CreateToken(appUser)
                        }
                        );
                }
                else
                {
                    return StatusCode(500, roles.Errors);
                }
            }
            else
            {
                return StatusCode(500, createUser.Errors);
            }
        }
        catch(Exception e)
        {
            return StatusCode(500, e);
        }
    }
}