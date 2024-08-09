using Microsoft.Maui.Controls;
using Messaging_App.ViewModels;
using Messaging_App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging_App.Views
{
    public partial class ConversationPage : ContentPage
    {
        public ConversationPage()
        {
            InitializeComponent();

            BindingContext = App.Services.GetRequiredService<ConversationViewModel>();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled; // Ensure flyout is disabled
        }
    }
}
