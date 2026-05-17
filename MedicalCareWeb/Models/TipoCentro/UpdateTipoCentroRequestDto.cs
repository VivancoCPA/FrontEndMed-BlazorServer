using System.ComponentModel.DataAnnotations;

namespace MedicalCareWeb.Models.TipoCentro
{
    public class UpdateTipoCentroRequestDto: CreateTipoCentroRequestDto
    {
        [Required]
        public int Id { get; set; }
    }
}
