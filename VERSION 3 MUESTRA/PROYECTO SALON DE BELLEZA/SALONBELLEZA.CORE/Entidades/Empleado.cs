using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("empleados")]
    public class Empleado
    {
        [Key]
        [Column("id_empleado")]
        public int IdEmpleado { get; set; }

        // Clave foránea hacia Personas
        [Column("id_persona")]
        public int IdPersona { get; set; }

        [Column("tipo_empleado")]
        [MaxLength(50)]
        public string? TipoEmpleado { get; set; }

        [Column("fecha_ingreso")]
        public DateTime? FechaIngreso { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }
    }
}