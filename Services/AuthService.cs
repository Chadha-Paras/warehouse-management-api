using Microsoft.EntityFrameworkCore;
using warehouse_management_api.Data;
using warehouse_management_api.Models;
using warehouse_management_api.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;


public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    public async Task<string> Register(RegisterDto dto)
    {
        var user = new User
        {
            Email = dto.Email,
            Password = dto.Password // later we hash
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return "User registered";
    }
    public async Task<string?> Login(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == dto.Email && x.Password == dto.Password);

        if (user == null) return null;

        var claims = new[]
        {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(
            issuer: "warehouse-api",
            audience: "warehouse-users",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );
        //Temporary for debugging the error
        var keyString = _config["Jwt:Key"];
        var keyBytes = Encoding.UTF8.GetBytes(keyString);

        Console.WriteLine($"KEY: {keyString}");
        Console.WriteLine($"BYTE LENGTH: {keyBytes.Length}");

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}