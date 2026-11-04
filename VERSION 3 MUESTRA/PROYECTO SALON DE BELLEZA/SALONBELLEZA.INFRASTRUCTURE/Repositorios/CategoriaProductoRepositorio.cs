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

public class CategoriaProductoRepositorio : ICategoriaProductoRepositorio
{
    private readonly IMyConexion _conn;
    public CategoriaProductoRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> RegistrarCategoriaProductoAsync(string n, string? d) => EjecutarIdAsync("RegistrarCategoriaProducto", ("p_nombre", null, n), ("p_descripcion", null, d));

    public async Task<IEnumerable<CategoriaProducto>> ObtenerCategoriasProductoAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<CategoriaProducto>("ObtenerCategoriasProducto", commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarCategoriaProductoAsync(int idCategoria, string nombre, string? descripcion) => EjecutarAsync("ActualizarCategoriaProducto",
        ("p_id_categoria", null, idCategoria), ("p_nombre", null, nombre), ("p_descripcion", null, descripcion));

    public Task DesactivarCategoriaProductoAsync(int idCategoria) => EjecutarAsync("DesactivarCategoriaProducto", ("p_id_categoria", null, idCategoria));
}