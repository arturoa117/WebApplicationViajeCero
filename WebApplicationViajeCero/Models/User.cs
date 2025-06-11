using System.ComponentModel.DataAnnotations;

namespace WebApplicationViajeCero.Models
{
    public class User : BaseClass
    {
      
        [Required]
        public string Identification {  get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]

        public string Email { get; set; }

        [Required]
        public string CellPhone { get; set; }

        [Required]
        public string Zone { get; set; }
        
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
