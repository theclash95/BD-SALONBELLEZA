using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SALONBELLEZA.CORE.Entidades
{
    [Table("proveedores")]
    public class Proveedor
    {
        [Key]
        [Column("id_proveedor")]
        public int IdProveedor { get; set; }

        [Column("razon_social")]
        [MaxLength(100)]
        public string RazonSocial { get; set; } = string.Empty;

        [Column("cuit")]
        [MaxLength(20)]
        public string? Cuit { get; set; }

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
        [MaxLength(50)]
        public string? Activo { get; set; } // En tu BD es VARCHAR(50)
    }
}