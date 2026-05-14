using System.ComponentModel.DataAnnotations;

namespace MedicalCareWeb.Models.Aseguradora;

public class AseguradoraDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string PersonInCharge { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}
