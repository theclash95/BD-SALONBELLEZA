namespace SALONBELLEZA.API.DTOs
{
    public class CategoriaServicioDto
    {
        public int IdCategoriaServicio { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}