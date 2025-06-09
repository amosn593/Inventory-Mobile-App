using InventoryCleanApp.Pages;

namespace InventoryCleanApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
            Routing.RegisterRoute(nameof(InventoryPage), typeof(InventoryPage));

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"[UnhandledException] {e.ExceptionObject}");
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"[UnobservedTaskException] {e.Exception}");
                e.SetObserved();
            };
        }
    }
}
