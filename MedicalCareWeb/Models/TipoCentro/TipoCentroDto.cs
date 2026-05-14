namespace MedicalCareWeb.Models.TipoCentro;

public class TipoCentroDto
{
    public int Id { get; set; }
    public string name { get; set; } = null!;
    public bool isActive { get; set; } = true;
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}
