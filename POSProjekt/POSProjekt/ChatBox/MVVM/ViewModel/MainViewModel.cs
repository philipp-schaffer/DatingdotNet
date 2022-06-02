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
using System.Xml;
using System.Xml.Linq;

namespace ChatBox.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public SwipeViewModel svm { get;set; }

        public ObservableCollection<User> ChatUsers { get; set; }
        public ObservableCollection<UserModel> Users { get; set; }  
        private ObservableCollection<string> _messages;
        public ObservableCollection<string> Messages {
            get => _messages;
            set { 
            _messages = value;
                OnPropertyChanged();
                
            } }
        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand HelloCommand { get; set; }
        private Server _server;
       
        private User _selectedUser;
        private ObservableCollection<string> _chatMessages;
        public ObservableCollection<string> ChatMessages {
            get => _chatMessages;
            set
            {
                _chatMessages = value;
                OnPropertyChanged();
             
            }
        }
        public User SelectedUser
        {
            get => _selectedUser;
            set { 
                _selectedUser = value;
                OnPropertyChanged();
                LoadChat();
            }
        }

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
          
            _server = new Server();
            _chatMessages = new ObservableCollection<string>();
            Messages = new ObservableCollection<string>();

          
            _server.connectedEvent += UserConnected;
            _server.msgRecivedEvent += MessageRecived;
            _server.userDisconnectEvent += UserDisconnect;
            ConnectToServerCommand = new RelayCommand(o => ConnectAndLogin(), o => CanLogin());
            SendMessageCommand = new RelayCommand(o => Send(), o => !string.IsNullOrEmpty(Message));
            HelloCommand = new RelayCommand(o => Console.WriteLine("Hello world"), o => true);
           

        }

        public void LoadChat()
        {
            if(CurrentUser != null && SelectedUser != null) { 
            var list = new List<string>();
            using (var db = new DatingDB())
            {
                    Console.WriteLine(db.Chats.Count());
                    Console.WriteLine(db.Messages.Count());
                var chat = (from c in db.Chats
                            where c.UserId1.Equals(CurrentUser.UserId) && c.UserId2.Equals(SelectedUser.UserId)
                          select c).FirstOrDefault();

                    

                chat.Messages.ToList().ForEach(m => list.Add(m.Message1));
                
            }
            ChatMessages.Clear();
            list.ForEach(x=> ChatMessages.Add(x));
            }
        }

        public void Send()
        {
            _server.SendMessageToServer(MessageToXML().ToString());
            using (var db = new DatingDB())
            {
                Console.WriteLine("CHATSSSS/////");

                db.Chats.ToList().ForEach(chat => Console.WriteLine(chat.ChatId + "  usr 1:" + chat.UserId1 + "  User 2:" + chat.UserId2 + chat.Messages.Count()));
                /*für sender*/
                var correctChat1 = (from c in db.Chats
                                    where c.UserId1.Equals(CurrentUser.UserId) && c.UserId2.Equals(SelectedUser.UserId)
                                    select c).FirstOrDefault();
                /** für reciver**/
                var correctChat2 = (from c in db.Chats
                                    where c.UserId2.Equals(CurrentUser.UserId) && c.UserId1.Equals(SelectedUser.UserId)
                                    select c).FirstOrDefault();
                /*für sender*/
                correctChat1.Messages.Add(new Message()
                {
                    Chat = correctChat1,
                    ChatId = correctChat1.ChatId,
                    Message1 = Message
                });
                /** für reciver **/
                correctChat2.Messages.Add(new Message()
                {
                    Chat = correctChat1,
                    ChatId = correctChat1.ChatId,
                    Message1 = Message
                });
                
                db.SaveChanges();
                LoadChat();
            }
        }

        public XElement MessageToXML()
        {
            XElement xml = new XElement("Message", 
                new XAttribute("Sender",CurrentUser.UserId),
                new XAttribute("Receiver",SelectedUser.UserId),
                new XAttribute("Time", DateTime.Now),
                Message
                
                );
            Console.WriteLine(xml);
            return xml;
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
                            UserId = user.UserId,
                            ChatUserId1Nav = user.ChatUserId1Navigations,
                            ChatUserId2Nav = user.ChatUserId2Navigations
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
                if (matchedChat != null )
                {
                    foreach (var chat in matchedChat)
                    {
                        if (chat.UserId1.Equals(CurrentUser.UserId))
                        {
                            persons.Add(chat.UserId2Navigation);
                        }

                    }
                    persons.ForEach(x => ChatUsers.Add(x));
                }

         }
            svm = new SwipeViewModel(CurrentUser);
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

        public void MessageRecived()
        {
           
            var msg = _server.PacketReader.ReadMessage();
            
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));
          
            var xdoc = XDocument.Parse(msg);
            if (xdoc.Element("Message").Attribute("Receiver").Value.Equals(CurrentUser.UserId.ToString()) &&
                xdoc.Element("Message").Attribute("Sender").Value.Equals(SelectedUser.UserId.ToString()
                )) {
                var newmsg = xdoc.Element("Message").Value;
                Application.Current.Dispatcher.Invoke(() => ChatMessages.Add(newmsg));
            }
            

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
