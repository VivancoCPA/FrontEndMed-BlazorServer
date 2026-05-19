namespace MedicalCareWeb.Models.Medico;

public class MedicoCentroAfiliadoDto
{
    public Guid Id { get; set; }//Id Centro
    public string Name { get; set; } = string.Empty;
    public string? OfficeNumber { get; set; }
    public string? WorkSchedule { get; set; }
}
