using System.Data;
using MySqlConnector;
using SALONBELLEZA.INFRASTRUCTURE.Contexto;

namespace SALONBELLEZA.TEST
{
    public class TestConexion
    {
        [Fact]
        public async Task Test1Conexion()
        {
            string connectionString =
                "Server=127.0.0.1;User Id=root;Password=;Port=3306;";

            MyConexion conexion = new MyConexion(connectionString);

            using var conn = await conexion.ObtenerConexionAsync();

            Assert.NotNull(conn);
            Assert.Equal(ConnectionState.Open, conn.State);

            using var mysqlConn = (MySqlConnection)conn;

            using var cmd = new MySqlCommand("SHOW DATABASES;", mysqlConn);

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Console.WriteLine(reader.GetString(0));
            }
        }
    }
}