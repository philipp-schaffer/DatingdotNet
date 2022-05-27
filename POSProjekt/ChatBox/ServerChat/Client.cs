using ServerChat.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat
{
    public class Client
    {
        public string Username { get; set; }

        public Guid UsrGuid { get; set; }

        public TcpClient ClientSocket { get; set; }
        public PacketReader _packetReader { get; set; }

        public Client(TcpClient client)
        {
            ClientSocket = client;
            UsrGuid = Guid.NewGuid();

            _packetReader = new PacketReader(ClientSocket.GetStream());
            var opcode = _packetReader.ReadByte();
            
            Username = _packetReader.ReadMessage();
            Console.WriteLine($"{DateTime.Now}: Client has connected {Username}");
            Task.Run(() => Process());
        }

        public void Process()
        {
            while (true)
            {
                try
                {
                    var opcode = _packetReader.ReadByte();
                    switch (opcode)
                    {
                        case 5:
                            var msg = _packetReader.ReadMessage();
                            Console.WriteLine($"{DateTime.Now}: Message Recived! {msg}");
                            // old version of the orginal code
                            //  Program.BroadcastMessage($"{DateTime.Now}: {Username} {msg} ");
                            // there is no dateTime.Now or username cause this informations are already provided in xml msg 
                            Program.BroadcastMessage($" {msg} ");
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{UsrGuid.ToString()}: disconnected");
                    Program.BroadcastDisconnect(UsrGuid.ToString());
                    ClientSocket.Close();
                    break ;
                }
            }
        }
    }
}
