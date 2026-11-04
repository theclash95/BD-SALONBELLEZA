using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("categorias_servicio")]
    public class CategoriaServicio
    {
        [Key]
        [Column("id_categoria_servicio")]
        public int IdCategoriaServicio { get; set; }

        [Column("nombre")]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Column("descripcion")]
        [MaxLength(255)]
        public string? Descripcion { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;
    }
}