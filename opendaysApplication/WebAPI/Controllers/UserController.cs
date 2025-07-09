using System.Security.Claims;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities.Users;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public UserController(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    // POST: api/user/authenticate
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
    {
        var result = await _authService.Authenticate(request.Username, request.Password);
        
        if (!result.Success)
            return Unauthorized(new { message = result.Message });

        SetTokenCookie(result.RefreshToken);
        return Ok(new { result.Token, result.User });
    }

    // POST: api/user/refresh-token
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        var token = authHeader?.Split(" ").Last();

        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshToken))
            return BadRequest(new { message = "Token is required" });

        var result = await _authService.RefreshToken(token, refreshToken);
        
        if (!result.Success)
            return Unauthorized(new { message = result.Message });

        SetTokenCookie(result.RefreshToken);
        return Ok(new { result.Token });
    }

    // POST: api/user/revoke-token
    [HttpPost("revoke-token")]
    [Authorize]
    public async Task<IActionResult> RevokeToken()
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
            return BadRequest(new { message = "Invalid user" });

        await _authService.RevokeToken(username);
        return Ok(new { message = "Token revoked" });
    }

    // GET: api/user/me
    [HttpGet("me")]
    [Authorize]
    public IActionResult GetCurrentUser()
    {
        var userDto = new UserDto
        {
            Username = User.Identity?.Name,
            Role = User.FindFirst(ClaimTypes.Role)?.Value,
            PersonId = User.FindFirst("personId")?.Value
        };

        return Ok(userDto);
    }

    // CRUD operations for users
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult<IEnumerable<AUser>> GetAllUsers()
    {
        return Ok(_userRepository.ReadAll());
    }

    [HttpGet("{username}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<AUser> GetUserByUsername(string username)
    {
        var user = _userRepository.GetByUsername(username);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult<AUser> CreateUser([FromBody] AUser user)
    {
        var createdUser = _userRepository.Create(user);
        return CreatedAtAction(nameof(GetUserByUsername), new { username = createdUser.Username }, createdUser);
    }

    [HttpPut("{username}")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateUser(string username, [FromBody] AUser user)
    {
        if (username != user.Username) return BadRequest();
        _userRepository.Update(user);
        return NoContent();
    }

    [HttpDelete("{username}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteUser(string username)
    {
        var user = _userRepository.GetByUsername(username);
        if (user == null) return NotFound();
        _userRepository.Delete(user);
        return NoContent();
    }

    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7),
            SameSite = SameSiteMode.Strict,
            Secure = true // Enable in production with HTTPS
        };
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }
}

//TODO
//finish all Authentifications

//  TODO
// Figure out what jwt does
// there is some entries in appsettings, check if its correct