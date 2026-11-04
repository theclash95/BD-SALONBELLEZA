using System.Data;
namespace SALONBELLEZA.CORE.Interfaces;
public interface IMyConexion
{
    IDbConnection ObtenerConexion();
    Task<IDbConnection> ObtenerConexionAsync();
}