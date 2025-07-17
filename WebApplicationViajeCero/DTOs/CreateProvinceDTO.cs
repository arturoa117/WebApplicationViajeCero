using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using WebApplicationViajeCero.Models;


namespace WebApplicationViajeCero.DTOs
{
    public class CreateProvinceDTO
    {
        [Required]

        public string Name { get; set; } = String.Empty;

        [Required]
        public string Zone { get; set; } = String.Empty;

    }
}
