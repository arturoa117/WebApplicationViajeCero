using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.DTOs
{
    public class CreateUserDTO
    {
        
        public String? Identification { get; set; } = String.Empty;
        
        public String? Name { get; set; } = String.Empty;

        public String? LastName { get; set; } = String.Empty;

        public String? Email { get; set; } = String.Empty;

        public String? CellPhone { get; set; } = String.Empty;

        public String? Zone { get; set; } = String.Empty;

        public String? UserName { get; set; } = String.Empty;

        public String? Password { get; set; } = String.Empty;

        public int? ProvinceId { get; set; } = 0;

        public int? RoleId { get; set; } = 0;



    }
}
