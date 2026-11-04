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

public class EmpleadoRepositorio : IEmpleadoRepositorio
{
    private readonly IMyConexion _conn;
    public EmpleadoRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> RegistrarEmpleadoAsync(Persona p, string? tipo) => EjecutarIdAsync("RegistrarEmpleado",
        ("p_dni", null, p.Dni), ("p_nombre", null, p.Nombre), ("p_apellido", null, p.Apellido),
        ("p_telefono", null, p.Telefono), ("p_email", null, p.Email), ("p_direccion", null, p.Direccion), ("p_tipo_empleado", null, tipo));

    public async Task<IEnumerable<EmpleadoConsulta>> ObtenerEmpleadosAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<EmpleadoConsulta>("ObtenerEmpleados", commandType: CommandType.StoredProcedure);
    }

    public async Task<EmpleadoConsulta?> ObtenerEmpleadoPorIdAsync(int idEmpleado)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<EmpleadoConsulta>("ObtenerEmpleadoPorId", new { p_id_empleado = idEmpleado }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EmpleadoConsulta>> BuscarEmpleadosAsync(string termino)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<EmpleadoConsulta>("BuscarEmpleados", new { p_termino = termino }, commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarEmpleadoAsync(int idEmpleado, string? dni, string nombre, string apellido, string? telefono, string? email, string? direccion, string? tipoEmpleado) => EjecutarAsync("ActualizarEmpleado",
        ("p_id_empleado", null, idEmpleado), ("p_dni", null, dni), ("p_nombre", null, nombre),
        ("p_apellido", null, apellido), ("p_telefono", null, telefono), ("p_email", null, email), ("p_direccion", null, direccion), ("p_tipo_empleado", null, tipoEmpleado));

    public Task DesactivarEmpleadoAsync(int idEmpleado) => EjecutarAsync("DesactivarEmpleado", ("p_id_empleado", null, idEmpleado));
}