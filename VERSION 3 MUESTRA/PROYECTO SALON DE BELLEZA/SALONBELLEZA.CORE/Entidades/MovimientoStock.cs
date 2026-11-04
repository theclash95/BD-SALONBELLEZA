using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("movimiento_stock")]
    public class MovimientoStock
    {
        [Key]
        [Column("id_movimiento")]
        public int IdMovimiento { get; set; }

        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("id_detalle_venta")]
        public int? IdDetalleVenta { get; set; } // Puede ser nulo si es por compra

        [Column("tipo_movimiento")]
        [MaxLength(50)]
        public string? TipoMovimiento { get; set; }

        [Column("cantidad")]
        public int? Cantidad { get; set; }

        [Column("fecha_hora")]
        public DateTime? FechaHora { get; set; }

        [Column("motivo")]
        [MaxLength(255)]
        public string? Motivo { get; set; }

        [Column("stock_anterior")]
        public int? StockAnterior { get; set; }

        [Column("stock_posterior")]
        public int? StockPosterior { get; set; }
    }
}