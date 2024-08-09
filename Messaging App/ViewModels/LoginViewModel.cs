using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Messaging_App.Models;

namespace Messaging_App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private MessagingService _messagingService;
        private string _username;
        private string _password;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        public LoginViewModel()
        {
            _messagingService = new MessagingService();
            LoginCommand = new Command(async () => await LoginAsync());
            SignUpCommand = new Command(async () => await OnSignUpClicked());
        }

        private async Task LoginAsync()
        {
            var user = await _messagingService.LoginAsync(Username, Password);

            if (user != null)
            {
                // Set the logged-in user
                App.LoggedInUser = user;

                // Navigate to MainPage
                await Shell.Current.GoToAsync("MainPage");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }

        private async Task OnSignUpClicked()
        {
            await Shell.Current.GoToAsync("SignUpPage");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
