using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Messaging_App.Models;
using System.Diagnostics;

namespace Messaging_App.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            try
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MessagingApp.db3");
                Debug.WriteLine($"Database path: {dbPath}");
                _database = new SQLiteAsyncConnection(dbPath);
                _database.CreateTableAsync<User>().Wait();
                _database.CreateTableAsync<Message>().Wait();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception during DatabaseService initialization: {ex}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException}");
                }
                throw;
            }
        }

        // User methods
        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> GetUserAsync(string username)
        {
            return _database.Table<User>().FirstOrDefaultAsync(u => u.Username == username);
        }

        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        // Message methods
        public Task<List<Message>> GetMessagesAsync(string conversationId)
        {
            return _database.Table<Message>().Where(m => m.ConversationId == conversationId).ToListAsync();
        }

        public Task<int> SaveMessageAsync(Message message)
        {
            return _database.InsertAsync(message);
        }
        public Task<User> GetUserByIdAsync(int userId)
        {
            return _database.Table<User>().FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
