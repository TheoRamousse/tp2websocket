using System.Net.Sockets;
using System.Net;
using System;
using Server_WebSocket.Services;

class Program
{
    private static int _port = 5007;
    private static int _xMax = 700;
    private static int _yMax = 1000;
    private static string connectionString = "Server=localhost;Database=mysql;User=root;Password=;";

    public static async Task Main()
    {
        MyWebSocketServer ws = new MyWebSocketServer(_xMax, _yMax, new DatabaseService(connectionString, _xMax, _yMax));

        ws.Setup(_port);

        Console.WriteLine("Server started on port " + _port);
        ws.Start();

        Console.ReadKey();

        ws.Stop();
    }
}