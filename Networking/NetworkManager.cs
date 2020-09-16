using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chess.Networking
{
    public static class NetworkManager
    {
        private static int udpPort = 42069;
        private static UdpClient client;
        private static Thread networkingThread;

        public static void Initialize()
        {
            try
            {
                client = new UdpClient(udpPort, AddressFamily.InterNetwork);
                client.EnableBroadcast = true;
            }
            catch(Exception ex)
            {
                client = null;
            }

            networkingThread = new Thread(new ThreadStart(StartNetworkingThread));
            networkingThread.Start();
        }

        private static void StartNetworkingThread()
        {
            if (client == null) return;

            //client.Send(RequestData, new IPEndPoint(IPAddress.Broadcast, udpPort));
            DateTime timestamp = DateTime.UtcNow;

            while (true)
            {
                var senderEndpoint = new IPEndPoint(IPAddress.Any, 0);
                var rawData = client.Receive(ref senderEndpoint);

                var parsedData = Encoding.ASCII.GetString(rawData);

                // handle message here

                // client.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, udpPort));

                // client.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, udpPort));
            }
        }
    }
}
