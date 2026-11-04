using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("salon")]
    public class Salon
    {
        [Key]
        [Column("id_salon")]
        public int IdSalon { get; set; }

        [Column("nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [Column("direccion")]
        [MaxLength(100)]
        public string? Direccion { get; set; }

        [Column("telefono")]
        [MaxLength(30)]
        public string? Telefono { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("logo")]
        [MaxLength(255)]
        public string? Logo { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [Column("fecha_alta")]
        public DateTime? FechaAlta { get; set; }
    }
}