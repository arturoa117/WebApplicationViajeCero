using System.ComponentModel.DataAnnotations;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.DTOs
{
    public class CreateRequestDTO
    {
        [Required]  
        public Guid UserUuid { get; set; } = Guid.Empty;

        [Required(ErrorMessage = "El sexo es obligatorio.")]
        [RegularExpression("^[fm]$", ErrorMessage = "El sexo debe ser 'f' o 'm'.")]
        public char? Sex { get; set; }

        public string? Unavailable { get; set; } = String.Empty;

        public Guid? ServiceUuid { get; set; } = Guid.Empty;

        [Required]
        public Guid ProvinceUuid { get; set; } = Guid.Empty;
        public string? Incident { get; set; } = String.Empty;
        public Guid? ExtraOptionUuid { get; set; } = Guid.Empty;


    }
}
