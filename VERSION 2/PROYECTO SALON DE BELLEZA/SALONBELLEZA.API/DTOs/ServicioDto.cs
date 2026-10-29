namespace SALONBELLEZA.API.DTOs
{
    public class ServicioDto
    {
        public int IdServicio { get; set; }
        public int IdCategoriaServicio { get; set; }
        public string NombreCategoria { get; set; } = string.Empty; // Viene del JOIN
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int? DuracionMinuto { get; set; }
        public string? Estado { get; set; }
    }
}