using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("productos")]
    public class Producto
    {
        [Key]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        // Clave foránea hacia Categorias_Producto
        [Column("id_categoria_producto")]
        public int IdCategoriaProducto { get; set; }

        [Column("codigo")]
        [MaxLength(50)]
        public string? Codigo { get; set; }

        [Column("nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Column("descripcion")]
        public string? Descripcion { get; set; } // En BD es TEXT

        [Column("precio_costo")]
        public decimal? PrecioCosto { get; set; }

        [Column("precio_venta")]
        public decimal PrecioVenta { get; set; }

        [Column("stock_actual")]
        public int? StockActual { get; set; }

        [Column("stock_minimo")]
        public int? StockMinimo { get; set; }

        [Column("estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }

        [Column("fecha_alta")]
        public DateTime? FechaAlta { get; set; }
    }
}