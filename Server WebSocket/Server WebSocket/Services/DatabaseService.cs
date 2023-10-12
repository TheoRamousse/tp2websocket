using Server_WebSocket.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_WebSocket.Services
{
    public class DatabaseService
    {
        private string _tableName = "pixelwar";
        private DatabaseAccessLayer _dal;
        private int _xTotal;
        private int _yTotal;

        public DatabaseService(string connectionString, int x, int y)
        {
            _xTotal = x;
            _yTotal = y;
            _dal = new DatabaseAccessLayer(connectionString, _tableName);
        }

        public void Upsert(string el)
        {
            var split = el.Split(' ');
            _dal.Upsert(new Models.PixelEntity(Int32.Parse(split[0]), Int32.Parse(split[1]), split[2]));
        }

        public string[,] GetAll()
        {
            var result = new string[_xTotal, _yTotal];

            for (int x = 0; x < _xTotal; x++)
            {
                for (int y = 0; y < _yTotal; y++)
                {
                    _dal.GetAll().ToList().ForEach(el =>
                    {
                        result[el.X, el.Y] = el.Color;
                    });
                }
            }


            return result;
        }
    }
}
