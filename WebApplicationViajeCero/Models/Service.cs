using System.ComponentModel.DataAnnotations;

namespace WebApplicationViajeCero.Models
{
    public class Service : BaseClass
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public int InstitutionId { get; set; }
        public Institution Institution { get; set; } 

    }
}
