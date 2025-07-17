using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WebApplicationViajeCero.Models
{
    public class Province : BaseClass 
    {
        [Required]
        public string Name { get; set; }

        public enum Zones
        {
            [EnumMember(Value = "Metropolitana")]
            Metropolitana,

            [EnumMember(Value = "Sur")]
            Sur,

            [EnumMember(Value = "Este")]
            Este,

            [EnumMember(Value = "Norte")]
            Norte,

            [EnumMember(Value = "Noroeste")]
            Noroeste
        }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Zones Zone { get; set; }

    }
}
