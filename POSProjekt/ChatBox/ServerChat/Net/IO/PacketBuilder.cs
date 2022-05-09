﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat.Net.IO
{
    public class PacketBuilder
    {

        MemoryStream _ms;
        public PacketBuilder()
        {
            _ms = new MemoryStream();

        }
        public void WriteOpCode(byte upcode)
        {
            _ms.WriteByte(upcode);
        }

        public void WriteMessage(string msg)
        {
            var msgLenght = msg.Length;


            _ms.Write(BitConverter.GetBytes(msgLenght));

            _ms.Write(Encoding.ASCII.GetBytes(msg));
        }

        public byte[] GetPacketBytes()
        {
            return _ms.ToArray();
        }
    }
}
