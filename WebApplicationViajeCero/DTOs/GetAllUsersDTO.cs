using System.ComponentModel.DataAnnotations;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.DTOs
{
    public class GetAllUsersDTO
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
        
        public Guid guid { get; set; }

        [Required]
        public Role Role { get; set; }

        public Province Province { get; set; }

    }

}

