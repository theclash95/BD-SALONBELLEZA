using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("pago_membresia")]
    public class PagoMembresia
    {
        [Key]
        [Column("id_pago_membresia")]
        public int IdPagoMembresia { get; set; }

        [Column("id_cliente_membresia")]
        public int IdClienteMembresia { get; set; }

        [Column("fecha_pago")]
        public DateTime? FechaPago { get; set; }

        [Column("monto_pago")]
        public decimal? MontoPago { get; set; }

        [Column("metodo_pago")]
        [MaxLength(50)]
        public string? MetodoPago { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }

        [Column("referencia")]
        [MaxLength(255)]
        public string? Referencia { get; set; }
    }
}