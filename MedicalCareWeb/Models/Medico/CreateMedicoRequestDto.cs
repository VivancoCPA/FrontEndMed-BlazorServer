using MedicalCareWeb.Common;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MedicalCareWeb.Models.Medico
{
    public class CreateMedicoRequestDto
    {
        [Required]
        public string name { get; set; } = string.Empty;
        [Required]
        public string lastName { get; set; } = string.Empty;
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

        [NotMapped, JsonIgnore]
        public IBrowserFile? FotoArchivo { get; set; }
        [NotMapped, JsonIgnore]
        public ArchivoDTO? Archivo { get; set; }
        public List<MedicoCentroAfiliadoDto> Centers { get; set; } = [];
    }
}
