using ChatBox.Model;
using ChatBox.MVVM.Core;
using ChatBox.MVVM.Model;
using ChatBox.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatBox.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        
        public ObservableCollection<User> ChatUsers { get; set; }
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }
        private Server _server;




        private LoginViewModel _loginVM;
        public LoginViewModel LoginVM
        {
            get => _loginVM;
            set
            {
                _loginVM = value;
                OnPropertyChanged();

            }
        }
        private CurrentUserViewModel _currentUserVM;
        public CurrentUserViewModel CurrentUser
        {
            get => _currentUserVM;
            set
            {
                _currentUserVM = value;
                OnPropertyChanged();
            }

        }

        public string Message { get; set; }

        public MainViewModel()
        {
            _loginVM = new LoginViewModel();
            ChatUsers = new ObservableCollection<User>();
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();
            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.msgRecivedEvent += MessageRecived;
            _server.userDisconnectEvent += UserDisconnect;
            ConnectToServerCommand = new RelayCommand(o => ConnectAndLogin(), o => CanLogin());
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
           

        }

        public void ConnectAndLogin()
        {
            List<string> messageList = new List<string>();

            using (var db = new DatingDB())
            {
                if (db.Users.Any(x => x.Username.Equals(LoginVM.Username)))
                {
                    var user = db.Users.FirstOrDefault(x => x.Username.Equals(LoginVM.Username));
                   
                    if (user.Password.Equals(LoginVM.Password))
                    {
                        CurrentUser = new CurrentUserViewModel()
                        {
                            Username = user.Username,
                            Password = user.Password,
                            UserId = user.UserId
                        };
                        _server.ConnectToServer(CurrentUser.Username);
                    }

                }
                var messages = db.Messages.ToList();
                var users = db.Users.ToList();
                var chats = db.Chats.ToList();
              
           

                var matchedChat = from c in db.Chats
                                  where c.UserId1.Equals(CurrentUser.UserId) || c.UserId2.Equals(CurrentUser.UserId)
                                  select c;
             
                var persons = new List<User>();
                foreach(var chat in matchedChat)
                {
                    if (chat.UserId1.Equals(CurrentUser.UserId))
                    {
                        persons.Add(chat.UserId2Navigation);
                    }
                    
                }

                persons.ForEach(x => ChatUsers.Add(x));

         }
        }

        public bool CanLogin()
        {
            using (var db = new DatingDB())
            {
                if (db.Users.Any(x => x.Username.Equals(LoginVM.Username)))
                {
                    return true;
                }
                else { return false; }

            }
        }

        private void MessageRecived()
        {
            var msg = _server.PacketReader.ReadMessage();
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));

        }

        private void UserDisconnect()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID.Equals(uid)).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }

        public void UserConnected()
        {
            var user = new UserModel()
            {
                Username = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage()
            };
            if (!Users.Any(x => x.UID.Equals(user.UID)))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }
    }
}
