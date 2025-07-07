namespace WebApplicationViajeCero.DTOs
{
    public class CreateServiceDTO  
    {
        public string Name { get; set; } = String.Empty;
        public Guid InstitutionUuid { get; set; } = Guid.Empty;
    }
}
