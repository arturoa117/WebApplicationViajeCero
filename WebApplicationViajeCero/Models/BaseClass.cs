using System.ComponentModel.DataAnnotations;

namespace WebApplicationViajeCero.Models
{
    public class BaseClass
    {
        public int Id { get; set; }

        public Guid Uuid { get; set; } = Guid.NewGuid(); 
    }
}
