using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Messaging_App.Models;
using Microsoft.Maui.Controls;

namespace Messaging_App.ViewModels
{
    public class NewConversationViewModel : INotifyPropertyChanged
    {
        private MessagingService _messagingService;
        private string _searchQuery;

        public ObservableCollection<User> Users { get; set; }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                }
            }
        }

        public ICommand SearchCommand { get; }

        public NewConversationViewModel()
        {
            _messagingService = new MessagingService();
            Users = new ObservableCollection<User>();
            SearchCommand = new Command(async () => await SearchAsync());
        }

        private async Task SearchAsync()
        {
            var results = await _messagingService.SearchUsersAsync(SearchQuery);
            Users.Clear();
            foreach (var user in results)
            {
                Users.Add(user);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
