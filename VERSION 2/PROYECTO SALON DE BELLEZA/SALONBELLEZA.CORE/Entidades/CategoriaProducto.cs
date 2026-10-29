using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("categorias_producto")]
    public class CategoriaProducto
    {
        [Key]
        [Column("id_categoria_producto")]
        public int IdCategoriaProducto { get; set; }

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