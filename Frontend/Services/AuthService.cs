using System.Net.Http.Json;
using System.Text.Json;

namespace Frontend.Services;

public class LoginResult { public bool Success { get; set; } public string Message { get; set; } = ""; public string Nama { get; set; } = ""; public string Email { get; set; } = ""; }

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

    public async Task<LoginResult> Login(string email, string password)
    {
        var res = await _http.PostAsJsonAsync("http://localhost:5274/api/auth/login", new { email, password });
        var body = await res.Content.ReadAsStringAsync();
        if (!res.IsSuccessStatusCode) return new LoginResult { Success = false, Message = "Email atau password salah" };
        try {
            using var doc = JsonDocument.Parse(body);
            var root = doc.RootElement;
            return new LoginResult { Success = true, Message = "Login berhasil", Nama = root.GetProperty("nama").GetString() ?? "", Email = root.GetProperty("email").GetString() ?? "" };
        } catch { return new LoginResult { Success = true, Message = "Login berhasil", Nama = "", Email = email }; }
    }
}