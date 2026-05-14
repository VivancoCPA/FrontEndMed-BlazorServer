using System.ComponentModel.DataAnnotations;

namespace MedicalCareWeb.Models.TipoCentro
{
    public class CreateTipoCentroRequestDto
    {
        [Required]
        public string name { get; set; } = null!;
        public bool isActive { get; set; } = true;
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
