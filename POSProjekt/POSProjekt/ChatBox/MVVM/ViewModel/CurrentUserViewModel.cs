using ChatBox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBox.MVVM.ViewModel
{
    public class CurrentUserViewModel : BaseViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public virtual ICollection<Chat> ChatUserId1Nav { get; set; }
        public virtual ICollection<Chat> ChatUserId2Nav { get; set; }

        public CurrentUserViewModel()
        {
            ChatUserId1Nav= new List<Chat>();
            ChatUserId2Nav= new List<Chat>();
            
        }

        public User ToUser()
        {
            var user = new User()
            {
                Username = Username,
                Password = Password,
                UserId = UserId,
                ChatUserId1Navigations = ChatUserId1Nav.ToHashSet(),
                ChatUserId2Navigations = ChatUserId2Nav.ToHashSet()
            };

            return user;
        }
    }
}
