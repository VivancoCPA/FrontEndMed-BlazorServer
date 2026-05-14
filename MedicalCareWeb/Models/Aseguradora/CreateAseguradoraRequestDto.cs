using MedicalCareWeb.Common;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MedicalCareWeb.Models.Aseguradora;

public class CreateAseguradoraRequestDto
{
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

    [NotMapped, JsonIgnore]
    public IBrowserFile? FotoArchivo { get; set; }
    [NotMapped, JsonIgnore]
    public ArchivoDTO? Archivo { get; set; }
}
