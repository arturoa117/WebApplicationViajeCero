using System.ComponentModel.DataAnnotations;

namespace WebApplicationViajeCero.Models
{
    public class Role :BaseClass
    {
    
        [Required]
        public  string Description { get; set; }

    }
}
