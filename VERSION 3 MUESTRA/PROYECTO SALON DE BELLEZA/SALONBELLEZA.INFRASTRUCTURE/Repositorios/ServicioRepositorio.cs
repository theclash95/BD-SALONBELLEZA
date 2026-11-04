using MySqlConnector;
using Dapper;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;
using SALONBELLEZA.INFRASTRUCTURE.Contexto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SALONBELLEZA.INFRASTRUCTURE.Repositorios;

public class ServicioRepositorio : IServicioRepositorio
{
    private readonly IMyConexion _conn;
    public ServicioRepositorio(IMyConexion conn) => _conn = conn;

    private async Task<int> EjecutarIdAsync(string proc, params (string, string?, object?)[] ps)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand(proc, c) { CommandType = CommandType.StoredProcedure };
        foreach (var p in ps) cmd.Parameters.AddWithValue(p.Item1, p.Item3 ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
        using var q = new MySqlCommand("SELECT LAST_INSERT_ID();", c);
        var v = await q.ExecuteScalarAsync();
        return Convert.ToInt32(v);
    }

    private async Task EjecutarAsync(string proc, params (string, string?, object?)[] ps)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand(proc, c) { CommandType = CommandType.StoredProcedure };
        foreach (var p in ps) cmd.Parameters.AddWithValue(p.Item1, p.Item3 ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
    }

    public Task<int> RegistrarServicioAsync(Servicio s) => EjecutarIdAsync("RegistrarServicio",
        ("p_id_categoria", null, s.IdCategoriaServicio), ("p_nombre", null, s.Nombre), ("p_descripcion", null, s.Descripcion),
        ("p_precio", null, s.Precio), ("p_duracion", null, s.DuracionMinuto));

    public async Task<IEnumerable<ServicioConsulta>> ObtenerServiciosAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<ServicioConsulta>("ObtenerServicios", commandType: CommandType.StoredProcedure);
    }

    public async Task<ServicioConsulta?> ObtenerServicioPorIdAsync(int idServicio)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<ServicioConsulta>("ObtenerServicioPorId", new { p_id_servicio = idServicio }, commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarServicioAsync(int idServicio, int idCategoria, string nombre, string? descripcion, decimal precio, int? duracion) => EjecutarAsync("ActualizarServicio",
        ("p_id_servicio", null, idServicio), ("p_id_categoria", null, idCategoria), ("p_nombre", null, nombre),
        ("p_descripcion", null, descripcion), ("p_precio", null, precio), ("p_duracion", null, duracion));

    public Task DesactivarServicioAsync(int idServicio) => EjecutarAsync("DesactivarServicio", ("p_id_servicio", null, idServicio));
}