using Chess.Networking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Extensions
{
    public static class UdpClientExtensions
    {
        private static int Send(this UdpClient client, INetworkMessage message, IPEndPoint ep)
        {
            var msg = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            return client.Send(msg, msg.Length, ep);
        }
    }
}
