using System.Windows.Input;
using Microsoft.Maui.Controls;
using Messaging_App.Models;
using System.Threading.Tasks;
using Messaging_App.Services;

namespace Messaging_App.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly MessagingService _messagingService;
        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand SignUpCommand { get; }

        public SignUpViewModel()
        {
            _messagingService = new MessagingService();
            SignUpCommand = new Command(async () => await OnSignUpClicked());
        }

        private async Task OnSignUpClicked()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "Username, Password and Confirm Password are required.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return;
            }

            if (await _messagingService.UserExistsAsync(Username))
            {
                ErrorMessage = "Username already exists.";
                return;
            }

            // Create new user
            var newUser = new User
            {
                Username = Username,
                Password = Password
            };

            await _messagingService.SignUpAsync(newUser);

            // Navigate to the main page after sign-up
            await Shell.Current.GoToAsync("MainPage");
        }
    }
}
