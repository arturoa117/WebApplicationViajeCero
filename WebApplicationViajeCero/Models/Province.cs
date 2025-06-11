using System.ComponentModel.DataAnnotations;

namespace WebApplicationViajeCero.Models
{
    public class Province : BaseClass
    {
        [Required]
        public string Name { get; set; }

    }
}
