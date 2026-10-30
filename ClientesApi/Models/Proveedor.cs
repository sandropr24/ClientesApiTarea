using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesApi.Models
{
   [Table("proveedores")]
   public class Proveedor
   {
      [Key]
      [Column("id")]
      public int Id { get; set; }

      [Required, StringLength(11)]
      [Column("ruc")]
      public string Ruc { get; set; } = string.Empty;

      [Required, StringLength(150)]
      [Column("razonsocial")]
      public string RazonSocial { get; set; } = string.Empty;

      [Required, StringLength(9)]
      [Column("telefono")]
      public string Telefono { get; set; } = string.Empty;

      [Required, StringLength(150)]
      [Column("direccion")]
      public string Direccion { get; set; } = string.Empty;
   }
}


