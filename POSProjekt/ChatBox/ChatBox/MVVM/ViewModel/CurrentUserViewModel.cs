using ChatBox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBox.MVVM.ViewModel
{
    public class CurrentUserViewModel 
    {

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public CurrentUserViewModel()
        {

        }

        public User ToUser()
        {
            var user = new User()
            {
                Username = Username,
                Password = Password,
                UserId = UserId
            };

            return user;
        }
    }
}
