using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesApi.Models
{

    [Table("productos")]
    public class Producto
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required,StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required, StringLength(100)]
        [Column("descripcion")]
        public string Descripcion {  get; set; }

        [Required]
        [Column("precio", TypeName = "decimal(7,2)")]
        public double Precio { get; set; }

        [Required,]
        [Column("garantia")]
        public byte Garantia { get; set; }

        [StringLength(40)]
        [Column("numserie")]
        public string NumSerie { get; set; }


    }
}
