using InventoryCleanApp.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace InventoryCleanApp.Services;

public class AuthService
{
    //private const string AuthStateKey = "AuthState";
    private readonly HttpClient _httpClient;

    public AuthService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(ApplicationConstants.HttpClientName);
    }
    public static async Task<bool> IsAuthenticated()
    {
        try
        {
            var SerializedAuth = await SecureStorage.Default.GetAsync(ApplicationConstants.AuthKeyName);

            return !string.IsNullOrWhiteSpace(SerializedAuth);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        //await Task.Delay(2000);
        //var AuthState = Preferences.Default.Get<bool>(AuthStateKey, false);
        //return AuthState;
    }

    public async Task<HttpClient> GetAuthenticatedHttpclient()
    {
        try
        {
            var SerializedAuth = await SecureStorage.Default.GetAsync(ApplicationConstants.AuthKeyName);

            var userSession = JsonConvert.DeserializeObject<LoginResponse>(SerializedAuth!);

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userSession!.Token);

            return _httpClient;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<string?> Login(Login login)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<Login>("api/Authentication/login", login);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                
                LoginResponse? Auth = JsonConvert.DeserializeObject<LoginResponse>(content);
                var SerializedAuth = JsonConvert.SerializeObject(Auth);
                await SecureStorage.Default.SetAsync(ApplicationConstants.AuthKeyName, SerializedAuth);
                return null;
            }
            else
            {
                //var content = await response.Content.ReadAsStringAsync();
                return content;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        //Preferences.Default.Set<bool>(AuthStateKey, true);
    }
    public void Logout()
    {
        try
        {
            SecureStorage.Default.Remove(ApplicationConstants.AuthKeyName);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
