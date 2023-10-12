using MySqlConnector;
using Server_WebSocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_WebSocket.DataAccessLayer
{
    public class DatabaseAccessLayer
    {
        MySqlConnection _conn;
        string _tableName;
        public DatabaseAccessLayer(string connectionString, string tableName)
        {
            _conn = new MySqlConnection(connectionString);
            _tableName = tableName;
        }

        public void Upsert(PixelEntity entity)
        {
            string query = $"INSERT INTO {_tableName} (x, y, color) VALUES (@x, @y, @color) " +
                   "ON DUPLICATE KEY UPDATE color = @color";

            MySqlCommand command = new MySqlCommand(query, _conn);
            command.Parameters.AddWithValue("@tableName", _tableName);
            command.Parameters.AddWithValue("@x", entity.X);
            command.Parameters.AddWithValue("@y", entity.Y);
            command.Parameters.AddWithValue("@color", entity.Color);

            try
            {
                _conn.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Upsert réussi !");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'upsert : " + ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }

        public IEnumerable<PixelEntity> GetAll()
        {
            var result = new List<PixelEntity>();
            string query = $"SELECT * FROM {_tableName}";

            MySqlCommand command = new MySqlCommand(query, _conn);

            try
            {
                _conn.Open();

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PixelEntity(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de connexion : " + ex.Message);
            }
            finally
            {
                _conn.Close();
            }

            return result;

        }


        public void CloseConnection()
        {
            _conn.Close();
        }
    }
}
