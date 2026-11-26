using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    public AuthController(AppDbContext db) => _db = db;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest(new { message = "Email sudah terdaftar" });
        var user = new User { Nama = dto.Nama, Email = dto.Email, PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), CreatedAt = DateTime.Now };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return Ok(new { message = "Register berhasil" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "Email atau password salah" });
        return Ok(new { message = "Login berhasil", nama = user.Nama, email = user.Email });
    }
}