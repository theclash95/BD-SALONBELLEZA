using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("pagos")]
    public class Pago
    {
        [Key]
        [Column("id_pago")]
        public int IdPago { get; set; }

        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Column("fecha_pago")]
        public DateTime? FechaPago { get; set; }

        [Column("monto")]
        public decimal? Monto { get; set; }

        [Column("metodo_pago")]
        [MaxLength(50)]
        public string? MetodoPago { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }

        [Column("referencia")]
        [MaxLength(100)]
        public string? Referencia { get; set; }
    }
}