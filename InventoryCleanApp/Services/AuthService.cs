using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryCleanApp.Services;

public class AuthService
{
    private const string AuthStateKey = "AuthState";
    public async Task<bool> IsAuthenticated()
    {
        await Task.Delay(2000);
        var AuthState = Preferences.Default.Get<bool>(AuthStateKey, false);
        return AuthState;
    }

    public void Login()
    {
        Preferences.Default.Set<bool>(AuthStateKey, true);
    }
    public void Logout()
    {
        Preferences.Default.Remove(AuthStateKey);
    }
}
