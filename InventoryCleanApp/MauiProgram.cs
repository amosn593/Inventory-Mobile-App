using CommunityToolkit.Maui;
using InventoryCleanApp.Models;
using InventoryCleanApp.Pages;
using InventoryCleanApp.Services;
using InventoryCleanApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace InventoryCleanApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Logging.AddDebug();
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddTransient<LoadingPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<ResetPasswordPage>();
            builder.Services.AddTransient<InventoryPage>();
            builder.Services.AddTransient<StoreViewModel>();
            builder.Services.AddSingleton<ProductRepo>();

            builder.Services
                    .AddHttpClient(ApplicationConstants.HttpClientName, client =>
                    {
                        client.BaseAddress = new Uri(ApplicationConstants.Live_BaseURI);
                        client.Timeout = TimeSpan.FromSeconds(60);
                    });
            
            return builder.Build();
        }
    }
}
