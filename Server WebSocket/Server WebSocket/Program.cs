using System.Net.Sockets;
using System.Net;
using System;
using Server_WebSocket.Services;

class Program
{
    private static int port = 5007;

    public static async Task Main()
    {
        MyWebSocketServer ws = new MyWebSocketServer();

        ws.Setup(port);

        ws.Start();

        Console.ReadKey();

        ws.Stop();
    }
}