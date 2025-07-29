using System.ComponentModel.DataAnnotations;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.DTOs
{
    public class GetUsersDTO
    {
        [Required]
        public string Identification { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]

        public string Email { get; set; }

        [Required]
        public string CellPhone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required] 
        
        public Guid Uuid { get; set; }

        [Required]
        public string Role { get; set; } = String.Empty;

        public string Province { get; set; } = String.Empty;

    }

}

