using Scalar.AspNetCore;
using System.ComponentModel.DataAnnotations;

namespace ClientesApi.Models
{
    public class RegistroRequest
    {

        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required,MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
