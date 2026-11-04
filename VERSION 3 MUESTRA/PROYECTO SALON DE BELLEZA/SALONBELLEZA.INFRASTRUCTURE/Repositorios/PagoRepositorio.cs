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

public class PagoRepositorio : IPagoRepositorio
{
    private readonly IMyConexion _conn;
    public PagoRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> RegistrarPagoAsync(Pago p) => EjecutarIdAsync("RegistrarPago",
        ("p_id_venta", null, p.IdVenta), ("p_monto", null, p.Monto), ("p_metodo_pago", null, p.MetodoPago), ("p_referencia", null, p.Referencia));

    public async Task<IEnumerable<PagoConsulta>> ObtenerPagosPorVentaAsync(int idVenta)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<PagoConsulta>("ObtenerPagosPorVenta", new { p_id_venta = idVenta }, commandType: CommandType.StoredProcedure);
    }
}