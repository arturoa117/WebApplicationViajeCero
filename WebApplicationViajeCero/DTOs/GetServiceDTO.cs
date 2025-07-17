namespace WebApplicationViajeCero.DTOs
{
    public class GetServiceDTO
    {
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        
        public string InstitutionName { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
