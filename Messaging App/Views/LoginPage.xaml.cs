using Messaging_App.ViewModels;

namespace Messaging_App.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        }

    }
}
