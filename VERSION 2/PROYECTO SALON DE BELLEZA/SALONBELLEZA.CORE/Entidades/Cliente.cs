using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        // Clave foránea que une al Cliente con la tabla Personas
        [Column("id_persona")]
        public int IdPersona { get; set; }

        [Column("categoria_cliente")]
        [MaxLength(50)]
        public string? CategoriaCliente { get; set; }

        [Column("fecha_registro")]
        public DateTime? FechaRegistro { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }
    }
}