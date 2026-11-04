using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("detalle_compra")]
    public class DetalleCompra
    {
        [Key]
        [Column("id_detalle_compra")]
        public int IdDetalleCompra { get; set; }

        [Column("id_compra")]
        public int IdCompra { get; set; }

        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("precio_costo")]
        public decimal PrecioCosto { get; set; }
    }
}