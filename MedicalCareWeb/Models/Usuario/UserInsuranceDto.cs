namespace MedicalCareWeb.Models.Usuario;

public class UserInsuranceDto
{
    public Guid InsurerId { get; set; }
    public string InsurerName { get; set; }= string.Empty;
    public string? InsurerPhone { get; set; }
    public string? InsurerEmail { get; set; }
    public string? LogoUrl { get; set; }
}
