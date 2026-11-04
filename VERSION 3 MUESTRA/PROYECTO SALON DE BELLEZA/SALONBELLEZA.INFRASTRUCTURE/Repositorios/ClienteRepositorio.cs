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

public class ClienteRepositorio : IClienteRepositorio
{
    private readonly IMyConexion _conn;
    public ClienteRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> RegistrarClienteAsync(Persona p, string? cat) => EjecutarIdAsync("RegistrarCliente",
        ("p_dni", null, p.Dni), ("p_nombre", null, p.Nombre), ("p_apellido", null, p.Apellido),
        ("p_telefono", null, p.Telefono), ("p_email", null, p.Email), ("p_direccion", null, p.Direccion), ("p_categoria", null, cat));

    public async Task<IEnumerable<ClienteConsulta>> ObtenerClientesAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<ClienteConsulta>("ObtenerClientes", commandType: CommandType.StoredProcedure);
    }

    public async Task<ClienteConsulta?> ObtenerClientePorIdAsync(int idCliente)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<ClienteConsulta>("ObtenerClientePorId", new { p_id_cliente = idCliente }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ClienteConsulta>> BuscarClientesAsync(string termino)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<ClienteConsulta>("BuscarClientes", new { p_termino = termino }, commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarClienteAsync(int idCliente, string? dni, string nombre, string apellido, string? telefono, string? email, string? direccion) => EjecutarAsync("ActualizarCliente",
        ("p_id_cliente", null, idCliente), ("p_dni", null, dni), ("p_nombre", null, nombre),
        ("p_apellido", null, apellido), ("p_telefono", null, telefono), ("p_email", null, email), ("p_direccion", null, direccion));

    public Task DesactivarClienteAsync(int idCliente) => EjecutarAsync("DesactivarCliente", ("p_id_cliente", null, idCliente));
}