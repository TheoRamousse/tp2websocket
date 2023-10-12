using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_WebSocket.Services
{

    public class MyWebSocketServer : WebSocketServer
    {
        private int _xTotal;
        private int _yTotal;
        private DatabaseService _dbService;

        private string[,] _listOfPixels;



        public MyWebSocketServer(int x, int y, DatabaseService dbService): base()
        {
            _xTotal = x;
            _yTotal = y;
            _dbService = dbService;
            _listOfPixels = _dbService.GetAll();

            for (int i = 0; i < x * y; i++)
            {
                if(string.IsNullOrEmpty(_listOfPixels[i % x, i / y]))
                    _listOfPixels[i % x, i / y] = "white";
            }

            this.NewMessageReceived += ReceiveMessage;
        }

        private void ReceiveMessage(WebSocketSession session, string value)
        {
            if(value == "GET")
            {
                session.Send("GET " + PixelsAsString());
            }
            else {
                var splittedMsg = value.Split(" ");
                string color = splittedMsg[0];
                int x = int.Parse(splittedMsg[1]);
                int y = int.Parse(splittedMsg[2]);

                _listOfPixels[x, y] = color;

                var data = x + " " + y + " " + _listOfPixels[x, y];

                this.GetAllSessions().ToList().ForEach(session => session.Send(data));
                this._dbService.Upsert(data);
            }
        }

        private string PixelsAsString()
        {
            StringBuilder result = new StringBuilder();
            for (int x = 0; x < _xTotal; x++){
                Console.WriteLine("x : " + x);
                for (int y = 0; y < _yTotal; y++)
                {
                    Console.WriteLine("y : " + y);
                    result.Append(x).Append(" ").Append(y).Append(" ").Append(_listOfPixels[x, y]).Append(",");
                }
            }

            return result.ToString();
        }

    }
}
