using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Messaging_App.Models;
using Microsoft.Maui.Controls;

namespace Messaging_App.ViewModels
{
    public class SearchUsersViewModel : BaseViewModel
    {
        private readonly MessagingService _messagingService;
        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (SetProperty(ref _searchQuery, value))
                {
                    SearchCommand.Execute(null);
                }
            }
        }

        public ICommand SearchCommand { get; }

        public SearchUsersViewModel()
        {
            _messagingService = new MessagingService();
            SearchCommand = new Command(async () => await SearchAsync());
        }

        private async Task SearchAsync()
        {
            Debug.WriteLine($"Search query: {SearchQuery}");

            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                ClearSearchResults();
                return;
            }

            var users = await _messagingService.SearchUsersAsync(SearchQuery);
            Debug.WriteLine($"Found {users.Count} users");

            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
        private void ClearSearchResults()
        {
            Debug.WriteLine("Clearing search results");
            Users.Clear();
        }
    }
}
