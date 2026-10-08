namespace SALONBELLEZA.API.DTOs;

public class VentaProductoRequest
{
    public int IdCliente { get; set; }
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
}
