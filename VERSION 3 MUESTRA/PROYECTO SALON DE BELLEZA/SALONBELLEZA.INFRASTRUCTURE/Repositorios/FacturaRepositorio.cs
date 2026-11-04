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

public class FacturaRepositorio : IFacturaRepositorio
{
    private readonly IMyConexion _conn;
    public FacturaRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> GenerarFacturaAsync(Factura f) => EjecutarIdAsync("GenerarFactura",
        ("p_id_venta", null, f.IdVenta), ("p_numero_factura", null, f.NumeroFactura), ("p_tipo_comprobante", null, f.TipoComprobante));

    public async Task<IEnumerable<FacturaConsulta>> ObtenerFacturasAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<FacturaConsulta>("ObtenerFacturas", commandType: CommandType.StoredProcedure);
    }

    public async Task<FacturaConsulta?> ObtenerFacturaPorIdAsync(int idFactura)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<FacturaConsulta>("ObtenerFacturaPorId", new { p_id_factura = idFactura }, commandType: CommandType.StoredProcedure);
    }
}