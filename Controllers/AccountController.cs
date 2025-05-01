using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager,ITokenServices tokenServices,SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _tokenServices = tokenServices;
        _signInManager = signInManager;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized("invalid username or password is incrrect");
        }

        return Ok(
            new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenServices.CreateToken(user)
            }
        );
    }
}