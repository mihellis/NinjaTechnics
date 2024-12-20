using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Technico.Services;
using Technico.Dtos;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserService _userService;

    public AuthController(UserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponseDTO>> Login([FromBody] LoginDTO loginModel)
    {
        var user = await _userService.ValidateUserByEmail(loginModel.Email, loginModel.Password);

        // Replace this with your actual user validation logic
        if (user != null)
        {
            var token = GenerateJwtToken(user);
            return Ok(new { User = user, Token = token });
        }

        return Unauthorized();
    }

    private string GenerateJwtToken(UserResponseDTO user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "John Doe")
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
