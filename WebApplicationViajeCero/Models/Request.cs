using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebApplicationViajeCero.Models
{
    public class Request : BaseClass
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public char Sex { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
    }
}
