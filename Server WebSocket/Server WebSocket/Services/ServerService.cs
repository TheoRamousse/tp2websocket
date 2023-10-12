using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_WebSocket.Services
{
    public class ServerService
    {
        private List<WebSocket> _sockets = new List<WebSocket>();
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public ServerService()
        {
        }

        public async Task StartAsync(string httpListenerPrefix)
        {
            var httpListener = new HttpListener();
            httpListener.Prefixes.Add(httpListenerPrefix);
            httpListener.Start();

            Console.WriteLine("Server started.");

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                HttpListenerContext context = await httpListener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    ProcessWebSocketRequest(context);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }

            httpListener.Stop();
        }

        private void ProcessWebSocketRequest(HttpListenerContext context)
        {
            throw new NotImplementedException();
        }

        public async Task StopAsync()
        {
            _cancellationTokenSource.Cancel();

            foreach (var socket in _sockets)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server shutting down", CancellationToken.None);
                socket.Dispose();
            }
        }

        private async Task<string> ReceiveMessageAsync(WebSocket webSocket)
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[10000]);
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, _cancellationTokenSource.Token);

            if (result.CloseStatus.HasValue)
            {
                return null;
            }

            return Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
        }

        private async Task SendMessageAsync(WebSocket webSocket, string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, _cancellationTokenSource.Token);
        }
    }
}
