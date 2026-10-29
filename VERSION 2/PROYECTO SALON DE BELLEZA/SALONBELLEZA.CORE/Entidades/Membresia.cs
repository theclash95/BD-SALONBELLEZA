using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("membresias")]
    public class Membresia
    {
        [Key]
        [Column("id_membresia")]
        public int IdMembresia { get; set; }

        [Column("nombre_plan")]
        [MaxLength(100)]
        public string NombrePlan { get; set; } = string.Empty;

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("costo")]
        public decimal? Costo { get; set; }

        [Column("duracion_dias")]
        public int? DuracionDias { get; set; }

        [Column("porcentaje_descuento")]
        public decimal? PorcentajeDescuento { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }
    }
}
