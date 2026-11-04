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

public class MembresiaRepositorio : IMembresiaRepositorio
{
    private readonly IMyConexion _conn;
    public MembresiaRepositorio(IMyConexion conn) => _conn = conn;

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

    public Task<int> CrearMembresiaAsync(Membresia m) => EjecutarIdAsync("CrearMembresia",
        ("p_nombre_plan", null, m.NombrePlan), ("p_descripcion", null, m.Descripcion), ("p_costo", null, m.Costo),
        ("p_duracion_dias", null, m.DuracionDias), ("p_descuento", null, m.PorcentajeDescuento));

    public Task<int> AsignarMembresiaClienteAsync(ClienteMembresia cm) => EjecutarIdAsync("AsignarMembresiaCliente",
        ("p_id_cliente", null, cm.IdCliente), ("p_id_membresia", null, cm.IdMembresia), ("p_observacion", null, cm.Observacion));

    public async Task<IEnumerable<Membresia>> ObtenerMembresiasAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<Membresia>("ObtenerMembresias", commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ClienteMembresiaConsulta>> ObtenerClienteMembresiasAsync(int idCliente)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<ClienteMembresiaConsulta>("ObtenerClienteMembresias", new { p_id_cliente = idCliente }, commandType: CommandType.StoredProcedure);
    }

    public Task RegistrarPagoMembresiaAsync(int idClienteMembresia, decimal monto, string metodoPago, string referencia) => EjecutarAsync("RegistrarPagoMembresia",
        ("p_id_cliente_membresia", null, idClienteMembresia), ("p_monto", null, monto), ("p_metodo", null, metodoPago), ("p_referencia", null, referencia));
}