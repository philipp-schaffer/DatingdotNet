using System;
using System.Collections.Generic;

namespace ChatBox.Model
{
    public partial class User
    {
        public User()
        {
            ChatUserId1Navigations = new HashSet<Chat>();
            ChatUserId2Navigations = new HashSet<Chat>();
            Images = new HashSet<Image>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Chat> ChatUserId1Navigations { get; set; }
        public virtual ICollection<Chat> ChatUserId2Navigations { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
