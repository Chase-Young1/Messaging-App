using Messaging_App.ViewModels;

namespace Messaging_App.Views
{
    public partial class SearchUsersPage : ContentPage
    {
        public SearchUsersPage()
        {
            InitializeComponent();
            BindingContext = new SearchUsersViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled; // Ensure flyout is disabled
        }
    }
}
