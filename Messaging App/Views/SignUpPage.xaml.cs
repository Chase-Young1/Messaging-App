using Messaging_App.ViewModels;
using Microsoft.Maui.Controls;

namespace Messaging_App.Views
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = new SignUpViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled; // Ensure flyout is disabled
        }
    }
}
