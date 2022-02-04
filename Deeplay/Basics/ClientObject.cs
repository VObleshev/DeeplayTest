using Newtonsoft.Json;
using DataLayer.Communication;
using Server.Interfaces;
using Server.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server.Basics
{
    public class ClientObject
    {
        public Guid Id { get; set; }
        public NetworkStream Stream { get; set; }
        public string Name { get; set; } = string.Empty;
        private TcpClient _tcpClient;
        private ServerObject _server;

        public ClientObject(TcpClient tcpClient, ServerObject server)
        {
            _tcpClient = tcpClient;
            _server = server;
        }

        public void Process()
        {
            try
            {
                Stream = _tcpClient.GetStream();

                while (true)
                {                   
                    var requestString = GetRequest();

                    try
                    {
                        var request = JsonConvert.DeserializeObject<Request>(requestString);
                        // логика роутинга, обработки и получения нужных данных
                        if (request != null)
                        {
                            var response = new Router(request.Route).CallAction(request.Data);

                            SendResponse(response);
                        }
                    }
                    catch (Exception ex)
                    {
                        SendResponse(new Response("При обработке запроса произошла ошибка, смотрите в Response.Exception", false, ex));
                    }
                }
            }
            catch (Exception ex)
            {
                SendResponse(new Response("При обработке запроса произошла ошибка, смотрите в Response.Exception", false, ex));
            }
            finally
            {
                _server.RemoveConnection(this);
                Close();
            }
        }

        private string GetRequest()
        {
            BinaryReader reader = new BinaryReader(Stream, Encoding.UTF8, true);

            var request = reader.ReadString();
            reader.Close();

            return request;
        }

        private void SendResponse(IResponsible response)
        {
            var responseJson = JsonConvert.SerializeObject(response);

            try
            {
                BinaryWriter writer = new BinaryWriter(Stream, Encoding.UTF8, true);
                writer.Write(responseJson);

                writer.Flush();
                writer.Close();
            }
            catch (IOException ex)
            {
                if(ex.InnerException is SocketException)
                    this.Close();
            }
        }

        internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (_tcpClient != null)
                _tcpClient.Close();
        }
    }
}
