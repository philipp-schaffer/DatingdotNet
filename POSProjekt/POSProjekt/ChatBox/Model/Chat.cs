using System;
using System.Collections.Generic;

namespace ChatBox.Model
{
    public partial class Chat
    {
        public Chat()
        {
            Messages = new HashSet<Message>();
        }

        public int ChatId { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }

        public virtual User UserId1Navigation { get; set; } = null!;
        public virtual User UserId2Navigation { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; }
    }
}
