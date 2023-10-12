using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_WebSocket.Models
{
    public class PixelEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }

        public PixelEntity(int x, int y, string color)
        {
            X = x;
            Y = y;
            Color = color;
        }
    }
}
