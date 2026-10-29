using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("turnos")]
    public class Turno
    {
        [Key]
        [Column("id_turno")]
        public int IdTurno { get; set; }

        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("id_empleado")]
        public int IdEmpleado { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        // En C# TimeSpan es el equivalente a la columna TIME de MySQL
        [Column("hora")]
        public TimeSpan Hora { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }

        [Column("observacion")]
        public string? Observacion { get; set; } // En BD es TEXT

        [Column("fecha_creacion")]
        public DateTime? FechaCreacion { get; set; }
    }
}