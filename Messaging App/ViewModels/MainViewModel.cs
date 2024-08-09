using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Messaging_App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand GoToConversationsCommand { get; private set; }
        public ICommand GoToSearchUsersCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        public MainViewModel()
        {
            GoToConversationsCommand = new Command(OnGoToConversations);
            GoToSearchUsersCommand = new Command(OnGoToSearchUsers);
            LogoutCommand = new Command(OnLogout);
        }

        private async void OnGoToConversations()
        {
            // Navigate to the Conversation Page
            await Shell.Current.GoToAsync("ConversationPage");
        }

        private async void OnGoToSearchUsers()
        {
            // Navigate to the Search Users Page
            await Shell.Current.GoToAsync("SearchUsersPage");
        }

        private async void OnLogout()
        {
            // Navigate to the Login Page after logout
            await Shell.Current.GoToAsync("LoginPage");
        }
    }
}
