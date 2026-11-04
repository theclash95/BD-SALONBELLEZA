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

public class ProductoRepositorio : IProductoRepositorio
{
    private readonly IMyConexion _conn;
    public ProductoRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> RegistrarProductoAsync(Producto p) => EjecutarIdAsync("RegistrarProducto",
        ("p_id_categoria", null, p.IdCategoriaProducto), ("p_codigo", null, p.Codigo), ("p_nombre", null, p.Nombre),
        ("p_descripcion", null, p.Descripcion), ("p_precio_costo", null, p.PrecioCosto), ("p_precio_venta", null, p.PrecioVenta),
        ("p_stock", null, p.StockActual), ("p_stock_minimo", null, p.StockMinimo));

    public async Task<IEnumerable<ProductoConsulta>> ObtenerProductosAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<ProductoConsulta>("ObtenerProductos", commandType: CommandType.StoredProcedure);
    }

    public async Task<ProductoConsulta?> ObtenerProductoPorIdAsync(int idProducto)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<ProductoConsulta>("ObtenerProductoPorId", new { p_id_producto = idProducto }, commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarProductoAsync(int idProducto, int idCategoria, string codigo, string nombre, string? descripcion, decimal precioCosto, decimal precioVenta, int stockMinimo) => EjecutarAsync("ActualizarProducto",
        ("p_id_producto", null, idProducto), ("p_id_categoria", null, idCategoria), ("p_codigo", null, codigo),
        ("p_nombre", null, nombre), ("p_descripcion", null, descripcion), ("p_precio_costo", null, precioCosto),
        ("p_precio_venta", null, precioVenta), ("p_stock_minimo", null, stockMinimo));

    public Task DesactivarProductoAsync(int idProducto) => EjecutarAsync("DesactivarProducto", ("p_id_producto", null, idProducto));

    public Task<int> ActualizarStockProductoAsync(int id, int cantidad, string tipo, string? motivo) => EjecutarIdAsync("ActualizarStockProducto",
        ("p_id_producto", null, id), ("p_cantidad", null, cantidad), ("p_tipo_movimiento", null, tipo), ("p_motivo", null, motivo));

    public async Task<IEnumerable<MovimientoStockConsulta>> ObtenerMovimientosStockAsync(int idProducto)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        var sql = "SELECT id_movimiento as IdMovimiento, id_producto as IdProducto, tipo_movimiento as TipoMovimiento, cantidad as Cantidad, fecha_hora as FechaHora, motivo as Motivo, stock_anterior as StockAnterior, stock_posterior as StockPosterior FROM movimiento_stock WHERE id_producto = @id";
        return await conn.QueryAsync<MovimientoStockConsulta>(sql, new { id = idProducto });
    }
}