using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Messaging_App.Models;
using Microsoft.Maui.Controls;

namespace Messaging_App.ViewModels
{
    public class ConversationViewModel : BaseViewModel
    {
        private readonly MessagingService _messagingService;
        public ObservableCollection<MessageViewModel> Messages { get; } = new ObservableCollection<MessageViewModel>();

        private string _newMessageText;
        public string NewMessageText
        {
            get => _newMessageText;
            set
            {
                if (SetProperty(ref _newMessageText, value))
                {
                    ((Command)SendMessageCommand).ChangeCanExecute();
                }
            }
        }

        public ICommand SendMessageCommand { get; }

        public ConversationViewModel(MessagingService messagingService)
        {
            _messagingService = messagingService ?? throw new ArgumentNullException(nameof(messagingService));
            SendMessageCommand = new Command(async () => await SendMessageAsync(), CanSend);
            LoadMessages();
        }

        private bool CanSend()
        {
            return !string.IsNullOrWhiteSpace(NewMessageText);
        }

        private async void LoadMessages()
        {
            try
            {
                var messages = await _messagingService.GetMessagesAsync("default");
                foreach (var message in messages)
                {
                    var user = await _messagingService.GetUserByIdAsync(message.SenderId);
                    Messages.Add(new MessageViewModel
                    {
                        Username = user.Username,
                        Text = message.Text,
                        TimeSent = message.TimeSent
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception during LoadMessages: {ex}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException}");
                }
                throw;
            }
        }

        private async Task SendMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(NewMessageText))
                return;

            try
            {
                var sender = App.LoggedInUser;
                if (sender == null)
                {
                    Debug.WriteLine("Logged-in user is null.");
                    return;
                }

                Debug.WriteLine($"Sender: {sender.Username} (ID: {sender.Id})");

                var message = new Message
                {
                    SenderId = sender.Id,
                    Text = NewMessageText,
                    TimeSent = DateTime.Now
                };

                Debug.WriteLine($"Message before sending: {message.Text} from {message.SenderId} at {message.TimeSent}");

                await _messagingService.SendMessageAsync("default", message);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Messages.Add(new MessageViewModel
                    {
                        Username = sender.Username,
                        Text = message.Text,
                        TimeSent = message.TimeSent
                    });
                    NewMessageText = string.Empty;
                    ((Command)SendMessageCommand).ChangeCanExecute();
                });

                Debug.WriteLine("Message sent successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error sending message: {ex}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException}");
                }
                throw;
            }
        }
    }
}
