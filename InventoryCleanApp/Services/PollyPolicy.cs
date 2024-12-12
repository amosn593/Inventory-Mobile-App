using Polly;
using Polly.Extensions.Http;

namespace InventoryCleanApp.Services;

public static class PollyPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        // Define a retry policy: retry up to 3 times with exponential backoff
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        // Define a circuit breaker policy: break after 5 consecutive failures and keep broken for 30 seconds
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}
