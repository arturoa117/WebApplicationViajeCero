using System.ComponentModel.DataAnnotations;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.DTOs
{
    public class GetUsersDTO
    {
        public string Identification { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CellPhone { get; set; }

        public Guid Uuid { get; set; }

        public string Role { get; set; } = String.Empty;

        public string Province { get; set; } = String.Empty;

    }

}

