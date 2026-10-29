namespace SALONBELLEZA.API.DTOs
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string? Dni { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? CategoriaCliente { get; set; }
        public string? Estado { get; set; }
    }
}