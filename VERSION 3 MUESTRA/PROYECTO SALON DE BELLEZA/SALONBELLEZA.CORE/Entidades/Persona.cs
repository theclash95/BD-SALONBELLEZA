
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("personas")] // Nombre exacto de la tabla en MySQL
    public class Persona
    {
        [Key] // Le dice a Dapper que es la clave primaria
        [Column("id_persona")]
        public int IdPersona { get; set; }

        [Column("dni")]
        [MaxLength(10)]
        public string? Dni { get; set; }

        [Column("nombre")]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")]
        [MaxLength(50)]
        public string Apellido { get; set; } = string.Empty;

        [Column("telefono")]
        [MaxLength(20)]
        public string? Telefono { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("direccion")]
        [MaxLength(100)]
        public string? Direccion { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [Column("fecha_alta")]
        public DateTime? FechaAlta { get; set; }
    }
}