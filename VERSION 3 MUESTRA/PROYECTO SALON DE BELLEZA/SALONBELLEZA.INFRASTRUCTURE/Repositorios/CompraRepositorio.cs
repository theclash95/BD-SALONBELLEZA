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

public class CompraRepositorio : ICompraRepositorio
{
    private readonly IMyConexion _conn;
    public CompraRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> RegistrarCompraAsync(int ip, int i, int c, decimal pc, string? o) => EjecutarIdAsync("RegistrarCompra",
        ("p_id_proveedor", null, ip), ("p_id_producto", null, i), ("p_cantidad", null, c), ("p_precio_costo", null, pc), ("p_observacion", null, o));

    public async Task<IEnumerable<CompraConsulta>> ObtenerComprasAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<CompraConsulta>("ObtenerCompras", commandType: CommandType.StoredProcedure);
    }

    public async Task<CompraConsulta?> ObtenerCompraPorIdAsync(int idCompra)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<CompraConsulta>("ObtenerCompraPorId", new { p_id_compra = idCompra }, commandType: CommandType.StoredProcedure);
    }
}