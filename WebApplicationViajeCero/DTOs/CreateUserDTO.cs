using System.ComponentModel.DataAnnotations;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Identification { get; set; } = String.Empty;

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public string LastName { get; set; } = String.Empty;
        [Required]

        public string Email { get; set; } = String.Empty;
        [Required]

        public string CellPhone { get; set; } = String.Empty;
        [Required]

        public string Password { get; set; } = String.Empty;

        [Required]
        public Guid ProvinceUuid { get; set; } = Guid.Empty;

        [Required]
        public string Role { get; set; } = String.Empty;



    }
}
