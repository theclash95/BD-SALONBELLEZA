using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("cliente_membresia")]
    public class ClienteMembresia
    {
        [Key]
        [Column("id_cliente_membresia")]
        public int IdClienteMembresia { get; set; }

        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("id_membresia")]
        public int IdMembresia { get; set; }

        [Column("fecha_inicio")]
        public DateTime? FechaInicio { get; set; }

        [Column("fecha_fin")]
        public DateTime? FechaFin { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }

        [Column("observacion")]
        [MaxLength(255)]
        public string? Observacion { get; set; }
    }
}