using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using Messaging_App.Services;
using Messaging_App.ViewModels;
using Messaging_App.Views;
using System.Diagnostics;
using Messaging_App.Models;

namespace Messaging_App
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }
        public static User LoggedInUser { get; set; }
        public App()
        {
            InitializeComponent();

            try
            {
                // Register services
                var services = new ServiceCollection();
                ConfigureServices(services);

                Services = services.BuildServiceProvider();
                MainPage = Services.GetRequiredService<AppShell>();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Exception during app initialization: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw; // Re-throw to allow further handling/logging
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddSingleton<MessagingService>();
                services.AddTransient<ConversationViewModel>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<MainViewModel>();
                services.AddTransient<SearchUsersViewModel>();
                services.AddTransient<SignUpViewModel>();

                services.AddSingleton<AppShell>();
                services.AddTransient<ConversationPage>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception during service configuration: {ex}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException}");
                }
                throw;
            }
        }
        private void LogException(Exception ex)
        {
            Debug.WriteLine($"Exception: {ex}");
            if (ex.InnerException != null)
            {
                Debug.WriteLine($"Inner exception: {ex.InnerException}");
                LogException(ex.InnerException); // Recursively log inner exceptions
            }
        }
    }
}
