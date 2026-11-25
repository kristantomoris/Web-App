namespace Backend.DTOs;
public record RegisterDto(string Nama, string Email, string Password);
public record LoginDto(string Email, string Password);