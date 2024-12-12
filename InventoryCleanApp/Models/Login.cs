using System.ComponentModel.DataAnnotations;

namespace InventoryCleanApp.Models;

public class Login
{
    [Required, DataType(DataType.EmailAddress), EmailAddress]
    public string? Email { get; set; }
    [Required, DataType(DataType.Password)]
    public string? Password { get; set; }
}

public class LoginResponse
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}
