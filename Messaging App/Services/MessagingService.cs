using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messaging_App.Models;
using Messaging_App.Services;

public class MessagingService
{
    private readonly List<User> _users = new List<User>();
    private Dictionary<string, List<Message>> _cachedMessages;
    private readonly DatabaseService _databaseService;
    

    public MessagingService()
    {
        _databaseService = new DatabaseService();
        _cachedMessages = new Dictionary<string, List<Message>>();
    }

    // Adds a user to the system, ensuring not to add duplicates.
    public async Task SignUpAsync(User user)
    {
        var exists = await UserExistsAsync(user.Username);
        if (!exists)
        {
            await _databaseService.SaveUserAsync(user);
        }
    }
    public async Task<User> LoginAsync(string username, string password)
    {
        var user = await _databaseService.GetUserAsync(username);
        if (user != null && user.Password == password)
        {
            return user;
        }
        return null;
    }

    // Checks if a user exists based on the username.
    public async Task<bool> UserExistsAsync(string username)
    {
        var user = await _databaseService.GetUserAsync(username);
        return user != null;
    }

    public async Task<List<Message>> GetMessagesAsync(string conversationId)
    {
        if (!_cachedMessages.ContainsKey(conversationId) || !_cachedMessages[conversationId].Any())
        {
            var messages = await _databaseService.GetMessagesAsync(conversationId);
            _cachedMessages[conversationId] = messages;
        }
        return _cachedMessages[conversationId];
    }

    // Sends a message by adding it to the specified conversation.
    public async Task SendMessageAsync(string conversationId, Message message)
    {
        message.ConversationId = conversationId;
        await _databaseService.SaveMessageAsync(message);
        if (!_cachedMessages.ContainsKey(conversationId))
        {
            _cachedMessages[conversationId] = new List<Message>();
        }
        _cachedMessages[conversationId].Add(message);
    }

    // Gets the first user in the system, useful for setting the message sender or similar tasks.
    public async Task<User> GetFirstUserAsync()
    {
        var users = await GetAllUsersAsync();
        return users.FirstOrDefault();
    }

    // Retrieve all users in the system.
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _databaseService.GetUsersAsync();
    }
    public async Task<List<User>> SearchUsersAsync(string query)
    {
        // Fetch users from the database or in-memory list
        var users = await _databaseService.GetUsersAsync(); // Ensure this fetches all users
        return users.Where(u => u.Username.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _databaseService.GetUserByIdAsync(userId);
    }
}