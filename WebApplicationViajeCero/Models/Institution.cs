using System.ComponentModel.DataAnnotations;

namespace WebApplicationViajeCero.Models
{
    public class Institution : BaseClass
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Acronym { get; set; }

    }
}
