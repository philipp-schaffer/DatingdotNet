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
    public class MainViewModel
    {
        public LoginViewModel LoginVM { get; set; }
        
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }
        private Server _server;
        public string Username { get; set; }
        public string Message { get; set; }

        public MainViewModel()
        {
            LoginVM = new LoginViewModel();
            
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();
            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.msgRecivedEvent += MessageRecived;
            _server.userDisconnectEvent += UserDisconnect;
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));

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
            if (!Users.Any(x=> x.UID.Equals(user.UID)))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }
    }
}
