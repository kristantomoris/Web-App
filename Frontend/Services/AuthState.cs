namespace Frontend.Services;

public class AuthState
{
    public bool IsLoggedIn { get; private set; }
    public bool IsLocked { get; private set; }
    public string Nama { get; private set; } = "";
    public string Email { get; private set; } = "";
    public string Password { get; private set; } = "";

    public void Login(string nama, string email, string password)
    {
        IsLoggedIn = true; IsLocked = false;
        Nama = nama; Email = email; Password = password;
    }
    public void Lock() => IsLocked = true;
    public void Unlock() => IsLocked = false;
    public void Logout()
    {
        IsLoggedIn = false; IsLocked = false;
        Nama = ""; Email = ""; Password = "";
    }
}