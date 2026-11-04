using Dapper;
using MySqlConnector;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;
using SALONBELLEZA.INFRASTRUCTURE.Contexto;
using System.Data;

namespace SALONBELLEZA.INFRASTRUCTURE.Repositorios;

public class SalonRepositorio : ISalonRepositorio
{
    private readonly IMyConexion _conn;
    public SalonRepositorio(IMyConexion conn) => _conn = conn;

    private async Task EjecutarAsync(string proc, params (string, string?, object?)[] ps)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand(proc, c) { CommandType = CommandType.StoredProcedure };
        foreach (var p in ps) cmd.Parameters.AddWithValue(p.Item1, p.Item3 ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<Salon?> ObtenerSalonAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<Salon>("ObtenerSalon", commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarSalonAsync(int idSalon, string nombre, string direccion, string telefono, string email, string logo) => EjecutarAsync("ActualizarSalon",
        ("p_id_salon", null, idSalon), ("p_nombre", null, nombre), ("p_direccion", null, direccion),
        ("p_telefono", null, telefono), ("p_email", null, email), ("p_logo", null, logo));
}