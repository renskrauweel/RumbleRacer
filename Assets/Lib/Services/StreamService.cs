using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.Services
{
    public class StreamService
    {
        public ClientWebSocket ClientWebSocket { get; set; }
        
        public StreamService()
        {
            ClientWebSocket = new ClientWebSocket();
        }

        public async void StartStream(string input)
        {
            try
            {
                await ClientWebSocket.ConnectAsync(new Uri("ws://"+Constants.AI_IP+":"+Constants.AI_PORT), CancellationToken.None);
                await Send(ClientWebSocket, input);
                await Receive(ClientWebSocket);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR - {ex.Message}");
            }
        }

        private static async Task Send(ClientWebSocket socket, string data)
        {
            UnityEngine.Debug.Log("SENDING: " + data);
            await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(data)), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private static async Task Receive(ClientWebSocket socket)
        {
            var buffer = new ArraySegment<byte>(new byte[2048]);
            do
            {
                WebSocketReceiveResult result;
                using (var ms = new MemoryStream())
                {
                    do
                    {
                        result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                        ms.Write(buffer.Array, buffer.Offset, result.Count);
                    } while (!result.EndOfMessage);
            
                    if (result.MessageType == WebSocketMessageType.Close)
                        break;
            
                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms, Encoding.UTF8))
                        UnityEngine.Debug.Log("RECEIVED:" + await reader.ReadToEndAsync());
                }
            } while (true);
        }
    }
}