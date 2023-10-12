using System.Net.Sockets;
using System.Net;
using System;
using Server_WebSocket.Services;

class Program
{
    private static ServerService _server = new ServerService();

    public static async Task Main()
    {
        Console.WriteLine("Starting server");
        await _server.StartAsync("TODO");
    }
}