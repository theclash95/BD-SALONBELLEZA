using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("detalle_venta")]
    public class DetalleVenta
    {
        [Key]
        [Column("id_detalle_venta")]
        public int IdDetalleVenta { get; set; }

        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Column("id_producto")]
        public int? IdProducto { get; set; } // Nulo si es venta de servicio

        [Column("id_servicio")]
        public int? IdServicio { get; set; } // Nulo si es venta de producto

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("precio_unitario")]
        public decimal? PrecioUnitario { get; set; }

        [Column("descuento")]
        public decimal? Descuento { get; set; }

        [Column("subtotal")]
        public decimal? Subtotal { get; set; }
    }
}