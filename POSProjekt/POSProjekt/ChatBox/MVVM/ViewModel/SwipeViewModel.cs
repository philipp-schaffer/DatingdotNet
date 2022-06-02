using ChatBox.Model;
using ChatBox.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBox.MVVM.ViewModel
{
    public class SwipeViewModel : BaseViewModel
    {
        private static int counter = 0;
        public SwipeCurrentUserViewModel? SwipeCurrentUser { get; set; }
        public ObservableCollection<User> SwipeUsers { get; set; }
        public CurrentUserViewModel? CurrentUser { get; set; }
        public string SwipeURL { get; set; }
        public RelayCommand RightSwipe { get; set; }

        public SwipeViewModel(CurrentUserViewModel cuvm)
        {
            SwipeUsers = new ObservableCollection<User>();
            CurrentUser = cuvm;
            Load();

            RightSwipe = new RelayCommand(o => SwipeRight(), o => ( true));

        }

        public void SwipeRight()
        { 
          //errors
          // using(var db = new DatingDB())
            //{
            //    foreach (var user in SwipeUsers.ToList())
            //    {
            //        Console.WriteLine(user.Username);
            //        Console.WriteLine(user.Images.Count());
            //        Console.WriteLine(user.ChatUserId1Navigations.Count());
            //        Console.WriteLine(user.ChatUserId2Navigations.Count());
            //        Console.WriteLine(user.Password);
            //    }
            //}
        

            //var next = SwipeUsers.ToList()[counter++];
            //SwipeCurrentUser = new SwipeCurrentUserViewModel()
            //{
            //    UserId = next.UserId,
            //    Username = next.Username,
            //    ChatUserId1Nav = next.ChatUserId1Navigations,
            //    ChatUserId2Nav = next.ChatUserId2Navigations,
            //    Password = next.Password,
            //    Images = next.Images
            //};

            //if (SwipeCurrentUser.Images.Count() > 0)
            //{
            //    SwipeURL = SwipeCurrentUser.Images.FirstOrDefault().ImageUrl;
            //}
            //else
            //{
            //    SwipeURL = "https://raw.githubusercontent.com/oliwierjnowak/DatingdotNet/main/Images/kendall.jpg";
            //}



        }

        public void Load()
        {
            SwipeUsers.Clear();
            var users = new List<User>();
            using (var db = new DatingDB())
            {
                users = db.Users.Where(x => x.UserId != CurrentUser.UserId).ToList();
                foreach (var user in users)
                {
                    SwipeUsers.Add(user);
                }

                var first = SwipeUsers.ToList()[0];
                SwipeCurrentUser = new SwipeCurrentUserViewModel()
                {
                    UserId = first.UserId,
                    Username = first.Username,
                    ChatUserId1Nav = first.ChatUserId1Navigations,
                    ChatUserId2Nav = first.ChatUserId2Navigations,
                    Password = first.Password ,
                    Images = first.Images
                };

                
            }
            if (SwipeCurrentUser.Images.Count() > 0)
            {
                SwipeURL = SwipeCurrentUser.Images.FirstOrDefault().ImageUrl;
            }
            else
            {
                SwipeURL = "https://raw.githubusercontent.com/oliwierjnowak/DatingdotNet/main/Images/kendall.jpg";
            }
        }
    }
}
