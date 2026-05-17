using System.ComponentModel.DataAnnotations;

namespace MedicalCareWeb.Models.TipoEspecialidad;

public class TipoEspecialidadDto
{
    public int Id { get; set; }
    [Required]
    public string name { get; set; } = string.Empty;
    public bool isActive { get; set; } = true;
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }

}
