using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Messaging_App.Models
{
    [Table("Messages")]
    public class Message
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public string ConversationId { get; set; }

        public int SenderId { get; set; }
        public string Text { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
