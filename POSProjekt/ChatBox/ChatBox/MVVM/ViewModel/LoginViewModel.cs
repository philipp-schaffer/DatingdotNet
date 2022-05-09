using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBox.MVVM.ViewModel
{
    public class LoginViewModel
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            Username = "Oliwier";
            Password = "1234";
        }
    }
}
