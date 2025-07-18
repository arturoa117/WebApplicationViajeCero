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

        public string? Unavailable { get; set; } = String.Empty;

        [Required]
        public Guid ServiceUuid { get; set; } = Guid.Empty;

        [Required]
        public Guid ProvinceUuid { get; set; } = Guid.Empty;
        public string? Incident { get; set; } = String.Empty;
        public string? ExtraOptions { get; set; } = String.Empty;
    }
}
