using System.Security.Claims;
using API.Data;
using API.Models;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
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
    private readonly ApplicationDBcontext _context;

    public AccountController(UserManager<AppUser> userManager,
        ITokenServices tokenServices,
        SignInManager<AppUser> signInManager,
        ApplicationDBcontext context
        )
    {
        _userManager = userManager;
        _tokenServices = tokenServices;
        _signInManager = signInManager;
        _context = context;
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
                    var accessToken = _tokenServices.CreateToken(appUser);
                    var refreshToken = await _tokenServices.GenerateAndSaveRefreshToken(appUser);
                    return Ok(new NewUserDto
                        {
                            Token = accessToken,
                            RefreshToken = refreshToken
                            
                        });
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

        var accessToken = _tokenServices.CreateToken(user);
        var refreshToken = await _tokenServices.GenerateAndSaveRefreshToken(user);
        

        return Ok(
            new NewUserDto
            {
                Token = accessToken,
                RefreshToken = refreshToken,
            }
        );
    }


    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var stock = await _context.Stock
            .Where(s => s.CreatedUserById == userId)
            .Select(s => new
            {
                s.ID,
                s.Company,
                s.Title,
                s.Purchase,
                s.Industry,
                Comments = s.Comments.Select(c => new
                {
                    c.ID,
                    c.Title,
                    c.Content,
                    c.StockID
                })
            })
            .ToListAsync();
        
        var result = new
        {
            user.Id,
            user.UserName,
            user.Email,
            stocks = stock
        };

        return Ok(result);
    }

    
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> refreshToken([FromBody] RefreshTokenRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == request.RefreshToken);
        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow )
        {
            return Unauthorized("Invalid or expired refresh token");
        }

        var newAccessToken = _tokenServices.CreateToken(user);
        var newRefreshToken = await _tokenServices.GenerateAndSaveRefreshToken(user);

        return Ok(new
        {
            AccessToken = newAccessToken,
            refreshToken = newRefreshToken,
        });
    }
}