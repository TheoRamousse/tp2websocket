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

        private string[,] _listOfPixels = new string[_xTotal, _yTotal];
        private Thread t;



        public MyWebSocketServer(): base()
        {
            for(int i = 0; i < 700 * 1000; i++) _listOfPixels[i % 700, i / 1000] = "white";
            this.NewMessageReceived += ReceiveMessage;
            t = new(new ThreadStart(LoopSendRefresh));
        }

        public override bool Start()
        {
            t.Start();
            return base.Start();
        }

        public override void Stop()
        {
            t.Interrupt();
            base.Stop();
        }

        private void ReceiveMessage(WebSocketSession session, string value)
        {
            var splittedMsg = value.Split(" ");
            string color = splittedMsg[0];
            int x = int.Parse(splittedMsg[1]);
            int y = int.Parse(splittedMsg[2]);

            _listOfPixels[x, y] = color;
        }

        private void LoopSendRefresh()
        {
            while (true)
            {
                Thread.Sleep(1000);
                this.GetAllSessions().ToList().ForEach(session => session.Send(PixelsAsString()));
            }
        }

        private string PixelsAsString()
        {
            string result = "";
            for (int x = 0; x < _xTotal; x++){
                for (int y = 0; y < _yTotal; y++)
                {
                    result += x + " " + y + " " + _listOfPixels[x, y];
                }
            }

            return result;
        }

    }
}
