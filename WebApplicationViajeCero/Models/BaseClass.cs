using System.ComponentModel.DataAnnotations;

namespace WebApplicationViajeCero.Models
{
    public class BaseClass
    {
        [Key]
        public int Id { get; set; }

        public Guid Uuid { get; set; } = Guid.NewGuid();

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateModificated { get; set; } = DateTime.Now;

        public DateTime? DateDeleted { get; set; }

    }
}
