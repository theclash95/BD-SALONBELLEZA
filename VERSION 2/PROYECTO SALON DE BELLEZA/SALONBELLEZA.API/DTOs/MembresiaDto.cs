namespace SALONBELLEZA.API.DTOs
{
    public class MembresiaDto
    {
        public int IdMembresia { get; set; }
        public string NombrePlan { get; set; } = string.Empty;
        public decimal? Costo { get; set; }
        public int? DuracionDias { get; set; }
    }
}