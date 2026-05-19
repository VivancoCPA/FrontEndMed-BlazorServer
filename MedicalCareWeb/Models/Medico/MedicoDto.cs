using System.ComponentModel.DataAnnotations;

namespace MedicalCareWeb.Models.Medico
{
    public class MedicoDto
    {
    public Guid Id { get; set; }
    [Required]
    public string name { get; set; }= string.Empty;
    [Required]
    public string lastName { get; set; }= string.Empty ;
    [Required]
    public int SpecialtyId { get; set; }
    public string SpecialtyName { get; set; } = string.Empty;
    [Required]
    public string register { get; set; } = string.Empty;
    public string phone { get; set; } = string.Empty;
    [EmailAddress]
    public string email { get; set; } = string.Empty;
    public string photoUrl { get; set; } = string.Empty;
    public bool isVet { get; set; } = false;
    public bool isActive { get; set; } = true;
    public DateTime createdAt { get; set; } 
    public DateTime updatedAt { get; set; }
    public List<MedicoCentroAfiliadoDto> Centers { get; set; } = [];
    }
    
}
