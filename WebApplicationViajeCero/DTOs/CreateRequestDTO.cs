using System.ComponentModel.DataAnnotations;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.DTOs
{
    public class CreateRequestDTO
    {
        [Required]
        public Guid UserUuid { get; set; } = Guid.Empty;

        [Required]
        public char Sex { get; set; }

        [Required]
        public Guid ServiceUuid { get; set; } = Guid.Empty;

        [Required]
        public Guid ProvinceUuid { get; set; } = Guid.Empty;

    }
}
