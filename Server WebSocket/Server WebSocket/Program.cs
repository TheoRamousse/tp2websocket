using System.Net.Sockets;
using System.Net;
using System;
using Server_WebSocket.Services;
using Microsoft.Extensions.Configuration;

class Program
{
    private static int _port = 5007;
    private static int _xMax = 700;
    private static int _yMax = 1000;
    private static string _connectionString = "Server=localhost;Database=mysql;User=root;Password=;Port=3306";

    public static async Task Main()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        IConfigurationRoot configuration = builder.Build();

        var configs = configuration.GetSection("Application").AsEnumerable();

        _connectionString = configuration["ConnectionString"]!;
        _port = Int32.Parse(configuration["Port"]!);
        _xMax = Int32.Parse(configuration["XMax"]!);
        _yMax = Int32.Parse(configuration["YMax"]!);

        Console.WriteLine("Server starting, please wait 1 ou 2 minutes");
        MyWebSocketServer ws = new MyWebSocketServer(_xMax, _yMax, new DatabaseService(_connectionString, _xMax, _yMax));

        ws.Setup(_port);

        Console.WriteLine("Server started on port " + _port);
        ws.Start();

        Console.ReadKey();

        ws.Stop();
    }
}