﻿using CommunityToolkit.Maui;
using InventoryCleanApp.Models;
using InventoryCleanApp.Pages;
using InventoryCleanApp.Services;
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
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddTransient<LoadingPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<ResetPasswordPage>();
            
            builder.Services
                    .AddHttpClient(ApplicationConstants.HttpClientName, client =>
                    {
                        client.BaseAddress = new Uri(ApplicationConstants.Live_BaseURI);
                        client.Timeout = TimeSpan.FromSeconds(500);
                    });
                    //.AddPolicyHandler(PollyPolicy.GetRetryPolicy())
                    //.AddPolicyHandler(PollyPolicy.GetCircuitBreakerPolicy());


            return builder.Build();
        }
    }
}
