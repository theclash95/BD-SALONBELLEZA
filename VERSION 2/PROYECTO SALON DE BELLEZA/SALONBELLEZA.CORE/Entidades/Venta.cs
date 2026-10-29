using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("ventas")]
    public class Venta
    {
        [Key]
        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("id_turno")]
        public int? IdTurno { get; set; } // Puede ser nulo si es venta directa de producto

        [Column("fecha_hora")]
        public DateTime? FechaHora { get; set; }

        [Column("tipo_venta")]
        [MaxLength(50)]
        public string? TipoVenta { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }

        [Column("subtotal")]
        public decimal? Subtotal { get; set; }

        [Column("descuento")]
        public decimal? Descuento { get; set; }

        [Column("total")]
        public decimal? Total { get; set; }

        [Column("observacion")]
        [MaxLength(255)]
        public string? Observacion { get; set; }
    }
}