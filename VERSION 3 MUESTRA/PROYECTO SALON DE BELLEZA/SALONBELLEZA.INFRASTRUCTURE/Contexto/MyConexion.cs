using MySqlConnector;
using SALONBELLEZA.CORE.Interfaces;
using System.Data;
namespace SALONBELLEZA.INFRASTRUCTURE.Contexto;
public class MyConexion : IMyConexion
{
    private readonly string _cadena;
    public MyConexion(string cadena) => _cadena = cadena;
    public IDbConnection ObtenerConexion()
    {
        var c = new MySqlConnection(_cadena); c.Open(); return c;
    }
    public async Task<IDbConnection> ObtenerConexionAsync()
    {
        var c = new MySqlConnection(_cadena);
        await c.OpenAsync();
        return c;
    }
}