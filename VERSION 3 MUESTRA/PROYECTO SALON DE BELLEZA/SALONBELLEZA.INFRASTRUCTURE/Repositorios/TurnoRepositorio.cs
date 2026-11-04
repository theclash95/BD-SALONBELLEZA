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

public class TurnoRepositorio : ITurnoRepositorio
{
    private readonly IMyConexion _conn;
    public TurnoRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> CrearTurnoAsync(Turno t) => EjecutarIdAsync("CrearTurno",
        ("p_id_cliente", null, t.IdCliente), ("p_id_empleado", null, t.IdEmpleado), ("p_fecha", null, t.Fecha.Date),
        ("p_hora", null, t.Hora), ("p_observacion", null, t.Observacion));

    public Task<int> AgregarServicioATurnoAsync(int it, int is_, decimal d) => EjecutarIdAsync("AgregarServicioATurno",
        ("p_id_turno", null, it), ("p_id_servicio", null, is_), ("p_descuento", null, d));

    public async Task<IEnumerable<TurnoConsulta>> ObtenerTurnosAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<TurnoConsulta>("ObtenerTurnos", commandType: CommandType.StoredProcedure);
    }

    public async Task<TurnoConsulta?> ObtenerTurnoPorIdAsync(int idTurno)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        var sql = @"SELECT t.id_turno as IdTurno, t.id_cliente as IdCliente, CONCAT(pc.nombre, ' ', pc.apellido) as NombreCliente, 
                           t.id_empleado as IdEmpleado, CONCAT(pe.nombre, ' ', pe.apellido) as NombreEmpleado, 
                           t.fecha as Fecha, t.hora as Hora, t.estado as Estado, t.observacion as Observacion
                    FROM turnos t
                    INNER JOIN clientes c ON t.id_cliente = c.id_cliente
                    INNER JOIN personas pc ON c.id_persona = pc.id_persona
                    INNER JOIN empleados e ON t.id_empleado = e.id_empleado
                    INNER JOIN personas pe ON e.id_persona = pe.id_persona
                    WHERE t.id_turno = @id";
        return await conn.QueryFirstOrDefaultAsync<TurnoConsulta>(sql, new { id = idTurno });
    }

    public Task CambiarEstadoTurnoAsync(int idTurno, string estado) => EjecutarAsync("CambiarEstadoTurno", ("p_id_turno", null, idTurno), ("p_estado", null, estado));
}