using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace MedicalCareWeb.Models.Usuario;

public class UsuarioDto
{
    public Guid Id { get; set; }
    [EmailAddress]
    public string email { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string? phoneNumber { get; set; }
    public DateTime? dateOfBirth {  get; set; }
    public string? photoUrl { get; set; }
    public bool emailConfirmed { get; set; }
    public string? Address { get; set; }
    public bool isLockedOut { get; set; }
    public DateTime createAt { get; set; }
    public Guid? familyGroupId { get; set; }
    public string? familyGroupName { get; set; }
    public List<UserInsuranceDto>? Insurances { get; set; }


}
