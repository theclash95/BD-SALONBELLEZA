using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("facturas")]
    public class Factura
    {
        [Key]
        [Column("id_factura")]
        public int IdFactura { get; set; }

        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Column("numero_factura")]
        [MaxLength(20)] // En tu volcado decía VARCHAR(20) o VARCHAR(10), dejé 20 por seguridad
        public string? NumeroFactura { get; set; }

        [Column("fecha_emision")]
        public DateTime? FechaEmision { get; set; }

        [Column("tipo_comprobante")]
        [MaxLength(50)]
        public string? TipoComprobante { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }

        [Column("subtotal")]
        public decimal? Subtotal { get; set; }

        [Column("descuento")]
        public decimal? Descuento { get; set; }

        [Column("total")]
        public decimal? Total { get; set; }
    }
}