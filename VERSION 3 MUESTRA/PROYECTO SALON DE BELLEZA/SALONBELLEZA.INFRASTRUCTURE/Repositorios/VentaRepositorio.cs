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

public class VentaRepositorio : IVentaRepositorio
{
    private readonly IMyConexion _conn;
    public VentaRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> CrearVentaDesdeTurnoAsync(int idTurno, decimal descuento, string? observacion) => EjecutarIdAsync("CrearVentaDesdeTurno",
        ("p_id_turno", null, idTurno), ("p_descuento", null, descuento), ("p_observacion", null, observacion));

    public Task<int> RealizarVentaProductoAsync(int ic, int ip, int c) => EjecutarIdAsync("RealizarVentaProducto",
        ("p_id_cliente", null, ic), ("p_id_producto", null, ip), ("p_cantidad", null, c));

    public async Task<IEnumerable<VentaConsulta>> ObtenerVentasAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<VentaConsulta>("ObtenerVentas", commandType: CommandType.StoredProcedure);
    }

    public async Task<VentaConsulta?> ObtenerVentaPorIdAsync(int idVenta)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        var sql = @"SELECT v.id_venta as IdVenta, v.id_cliente as IdCliente, CONCAT(p.nombre, ' ', p.apellido) as NombreCliente,
                           v.fecha_hora as FechaHora, v.tipo_venta as TipoVenta, v.estado as Estado, v.subtotal as Subtotal, v.descuento as Descuento, v.total as Total, v.observacion as Observacion
                    FROM ventas v
                    INNER JOIN clientes c ON v.id_cliente = c.id_cliente
                    INNER JOIN personas p ON c.id_persona = p.id_persona
                    WHERE v.id_venta = @id";
        return await conn.QueryFirstOrDefaultAsync<VentaConsulta>(sql, new { id = idVenta });
    }
}