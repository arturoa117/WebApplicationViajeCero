using System.ComponentModel.DataAnnotations;
using WebApplicationViajeCero.Models;
using WebApplicationViajeCero.Validations;

namespace WebApplicationViajeCero.Filters
{
    public class FilterReport
    {
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }

        [ValidGenderArray(ErrorMessage = "Solo se permiten los valores 'f' o 'm', máximo dos.")]
        public char[]? Gender { get; set; }
    }
}
