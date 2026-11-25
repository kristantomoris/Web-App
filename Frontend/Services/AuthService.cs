using System.Net.Http.Json;

namespace Frontend.Services;

public class AuthService
{
    private readonly HttpClient _http;
    public AuthService(HttpClient http) => _http = http;

    public async Task<string> Register(string nama, string email, string password)
    {
        var res = await _http.PostAsJsonAsync("http://localhost:5274/api/auth/register", new { nama, email, password });
        var body = await res.Content.ReadAsStringAsync();
        return res.IsSuccessStatusCode ? "Berhasil register!" : "Gagal: " + body;
    }

    public async Task<string> Login(string email, string password)
    {
        var res = await _http.PostAsJsonAsync("http://localhost:5274/api/auth/login", new { email, password });
        var body = await res.Content.ReadAsStringAsync();
        return res.IsSuccessStatusCode ? "Login berhasil! Selamat datang." : "Gagal: " + body;
    }
}