using Microsoft.Build.Framework;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Identification {  get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

    }
}
