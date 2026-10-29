using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("servicios")]
    public class Servicio
    {
        [Key]
        [Column("id_servicio")]
        public int IdServicio { get; set; }

        // Clave foránea hacia Categorias_Servicio
        [Column("id_categoria_servicio")]
        public int IdCategoriaServicio { get; set; }

        [Column("nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Column("descripcion")]
        public string? Descripcion { get; set; } // En BD es TEXT

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("duracion_minuto")]
        public int? DuracionMinuto { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }
    }
}