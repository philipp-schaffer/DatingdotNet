using ChatBox.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBox.MVVM.ViewModel
{

    public class SwipeCurrentUserViewModel : BaseViewModel
    {
        public int? UserId { get; set; }
        public string? Username { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public virtual ICollection<Chat>? ChatUserId1Nav { get; set; }
        public virtual ICollection<Chat>? ChatUserId2Nav { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
       

        public SwipeCurrentUserViewModel()
        {
            ChatUserId1Nav = new List<Chat>();
            ChatUserId2Nav = new List<Chat>();
            Images=new List<Image>();
            Debug.WriteLine("git test");
        }
    }
}
