namespace WebApplicationViajeCero.DTOs
{
    public class UpdateServiceDTO  
    {
        public string? Name { get; set; } = String.Empty;
        public Guid? InstitutionUuid { get; set; } = Guid.Empty;
    }
}
