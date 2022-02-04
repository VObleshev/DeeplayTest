using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Basics
{
    public class ServerObject
    {
        private readonly string _host = "localhost";
        private readonly int _port = 5050;
        TcpListener tcpListener;
        public List<ClientObject> clients = new List<ClientObject>();

        public ServerObject()
        {
        }

        public ServerObject(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void AddConnection(ClientObject client) => clients.Add(client);

        public void RemoveConnection(ClientObject client)
        {
            var clientExist = clients.Where(c => c.Id == client.Id).FirstOrDefault();
            if (clientExist != null)
                clients.Remove(clientExist);
        }

        internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, _port);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключения...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Disconnect();
            }
        }

        public void Disconnect()
        {
            tcpListener.Stop();

            foreach (var client in clients)
            {
                client.Close();
            }

            Environment.Exit(0);
        }
    }
}
