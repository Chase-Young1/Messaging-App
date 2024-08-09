using Messaging_App.Views;

namespace Messaging_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(SearchUsersPage), typeof(SearchUsersPage));
            Routing.RegisterRoute(nameof(ConversationPage), typeof(ConversationPage));

            //Disables the hamburger navigation bar
            this.FlyoutBehavior = FlyoutBehavior.Disabled;
        }
    }
}
