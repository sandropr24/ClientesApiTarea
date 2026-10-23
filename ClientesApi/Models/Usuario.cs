using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClientesApi.Models
{

    [Table("Usuario")]
    public class Usuario
    {

        [Key]
        [Column("id")]
        public int  Id { get; set; }

        [Required, StringLength(50)]
        [Column("username")]

        public string Username { get; set; } = string.Empty;

        [Required, StringLength(200)]
        [Column("passwordhash")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required, StringLength(30)]
        [Column("rol")]
        public string Rol { get; set; } = "usuario";
    }
}
