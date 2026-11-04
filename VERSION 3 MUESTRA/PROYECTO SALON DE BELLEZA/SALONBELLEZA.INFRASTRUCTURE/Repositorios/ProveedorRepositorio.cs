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

public class ProveedorRepositorio : IProveedorRepositorio
{
    private readonly IMyConexion _conn;
    public ProveedorRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> RegistrarProveedorAsync(Proveedor p) => EjecutarIdAsync("RegistrarProveedor",
        ("p_razon_social", null, p.RazonSocial), ("p_cuit", null, p.Cuit), ("p_telefono", null, p.Telefono), ("p_email", null, p.Email), ("p_direccion", null, p.Direccion));

    public async Task<IEnumerable<Proveedor>> ObtenerProveedoresAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<Proveedor>("ObtenerProveedores", commandType: CommandType.StoredProcedure);
    }

    public async Task<Proveedor?> ObtenerProveedorPorIdAsync(int idProveedor)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<Proveedor>("ObtenerProveedorPorId", new { p_id_proveedor = idProveedor }, commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarProveedorAsync(int idProveedor, string razonSocial, string cuit, string telefono, string email, string direccion) => EjecutarAsync("ActualizarProveedor",
        ("p_id_proveedor", null, idProveedor), ("p_razon_social", null, razonSocial), ("p_cuit", null, cuit), ("p_telefono", null, telefono), ("p_email", null, email), ("p_direccion", null, direccion));

    public Task DesactivarProveedorAsync(int idProveedor) => EjecutarAsync("DesactivarProveedor", ("p_id_proveedor", null, idProveedor));
}