using Server.Basics;
using Server.ConfigurationApp;
using System;
using System.Threading;

namespace Server
{
    internal class Program
    {
        static ServerObject server; // сервер
        static Thread listenThread; // потока для прослушивания

        static void Main(string[] args)
        {
            // Add Dependency Injections
            new ConfigureServices().Build();

            try
            {
                server = new ServerObject();
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start();
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
