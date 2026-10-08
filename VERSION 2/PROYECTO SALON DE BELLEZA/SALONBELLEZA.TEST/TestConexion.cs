using System.Data;
using SALONBELLEZA.INFRASTRUCTURE.Contexto;
namespace SALONBELLEZA.TEST;
public class TestConexion
{
    [Fact]
    public async Task DebeConectarConSalon()
    {
        var cs="Server=localhost;Database=salonbellezabd;User Id=root;Password=;Port=3306;";
        var conexion=new MyConexion(cs);
        using var conn=await conexion.ObtenerConexionAsync();
        Assert.Equal(ConnectionState.Open,conn.State);
    }
}