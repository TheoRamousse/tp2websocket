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
        private const int _xTotal = 700;
        private const int _yTotal = 1000;
        private bool _stopThread = false;

        private string[,] _listOfPixels = new string[_xTotal, _yTotal];



        public MyWebSocketServer(): base()
        {
            for(int i = 0; i < 700 * 1000; i++) _listOfPixels[i % 700, i / 1000] = "white";
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

                this.GetAllSessions().ToList().ForEach(session => session.Send(x + " " + y + " " + _listOfPixels[x, y]));
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
