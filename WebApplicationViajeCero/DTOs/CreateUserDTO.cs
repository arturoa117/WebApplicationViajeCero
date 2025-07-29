using System.ComponentModel.DataAnnotations;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.DTOs
{
    public class CreateUserDTO
    {
        
        public string Identification { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;
      
        public string CellPhone { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public string Province { get; set; } = String.Empty;

        public string Role { get; set; } = String.Empty;



    }
}
