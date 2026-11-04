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

public class CategoriaServicioRepositorio : ICategoriaServicioRepositorio
{
    private readonly IMyConexion _conn;
    public CategoriaServicioRepositorio(IMyConexion conn) => _conn = conn;

    private async Task EjecutarAsync(string proc, params (string, string?, object?)[] ps)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand(proc, c) { CommandType = CommandType.StoredProcedure };
        foreach (var p in ps) cmd.Parameters.AddWithValue(p.Item1, p.Item3 ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> RegistrarCategoriaServicioAsync(string nombre, string? descripcion)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand(@"INSERT INTO Categorias_Servicio (nombre, descripcion, activo) VALUES (@nombre, @descripcion, TRUE);", c);
        cmd.Parameters.AddWithValue("@nombre", nombre);
        cmd.Parameters.AddWithValue("@descripcion", descripcion ?? (object)DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
        using var cmdId = new MySqlCommand("SELECT LAST_INSERT_ID();", c);
        return Convert.ToInt32(await cmdId.ExecuteScalarAsync());
    }

    public async Task<IEnumerable<CategoriaServicio>> ObtenerCategoriasServicioAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<CategoriaServicio>("ObtenerCategoriasServicio", commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarCategoriaServicioAsync(int idCategoria, string nombre, string? descripcion) => EjecutarAsync("ActualizarCategoriaServicio",
        ("p_id_categoria", null, idCategoria), ("p_nombre", null, nombre), ("p_descripcion", null, descripcion));

    public Task DesactivarCategoriaServicioAsync(int idCategoria) => EjecutarAsync("DesactivarCategoriaServicio", ("p_id_categoria", null, idCategoria));
}