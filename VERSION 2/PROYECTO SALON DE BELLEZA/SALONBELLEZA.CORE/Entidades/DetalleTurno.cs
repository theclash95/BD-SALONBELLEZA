using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("detalle_turno")]
    public class DetalleTurno
    {
        [Key]
        [Column("id_detalle_turno")]
        public int IdDetalleTurno { get; set; }

        [Column("id_turno")]
        public int IdTurno { get; set; }

        [Column("id_servicio")]
        public int IdServicio { get; set; }

        [Column("precio_unitario")]
        public decimal? PrecioUnitario { get; set; }

        [Column("descuento")]
        public decimal? Descuento { get; set; }

        [Column("subtotal")]
        public decimal? Subtotal { get; set; }
    }
}