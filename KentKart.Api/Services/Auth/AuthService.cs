using KentKart.Api.Data;
using KentKart.Api.DTOs.Auth;
using KentKart.Api.Entities;
using KentKart.Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace KentKart.Api.Services.Auth;

public class AuthService : IAuthService
{
    private readonly KentKartDbContext _context;
    private readonly JwtHelper _jwtHelper;

    public AuthService(KentKartDbContext context, JwtHelper jwtHelper)
    {
        _context = context;
        _jwtHelper = jwtHelper;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        if (string.IsNullOrWhiteSpace(registerDto.FirstName) ||
            string.IsNullOrWhiteSpace(registerDto.LastName) ||
            string.IsNullOrWhiteSpace(registerDto.Email) ||
            string.IsNullOrWhiteSpace(registerDto.Password))
        {
            throw new Exception("Ad, soyad, email ve şifre alanları zorunludur.");
        }

        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == registerDto.Email);

        if (emailExists)
        {
            throw new Exception("Bu email adresi zaten kullanılıyor.");
        }

        var userRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == "User");

        if (userRole == null)
        {
            throw new Exception("User rolü veritabanında bulunamadı.");
        }

        var user = new User
        {
            FirstName = registerDto.FirstName.Trim(),
            LastName = registerDto.LastName.Trim(),
            Email = registerDto.Email.Trim().ToLower(),
            PhoneNumber = registerDto.PhoneNumber,
            RoleId = userRole.RoleId,
            Role = userRole,
            IsActive = true
        };

        user.PasswordHash = PasswordHelper.HashPassword(user, registerDto.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _jwtHelper.GenerateToken(user);

        return new AuthResponseDto
        {
            UserId = user.UserId,
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Role = user.Role.Name,
            Token = token
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        if (string.IsNullOrWhiteSpace(loginDto.Email) ||
            string.IsNullOrWhiteSpace(loginDto.Password))
        {
            throw new Exception("Email ve şifre alanları zorunludur.");
        }

        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email.Trim().ToLower());

        if (user == null)
        {
            throw new Exception("Email veya şifre hatalı.");
        }

        if (!user.IsActive)
        {
            throw new Exception("Bu kullanıcı hesabı aktif değildir.");
        }

        var passwordIsValid = PasswordHelper.VerifyPassword(user, loginDto.Password);

        if (!passwordIsValid)
        {
            throw new Exception("Email veya şifre hatalı.");
        }

        var token = _jwtHelper.GenerateToken(user);

        return new AuthResponseDto
        {
            UserId = user.UserId,
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Role = user.Role.Name,
            Token = token
        };
    }
}

