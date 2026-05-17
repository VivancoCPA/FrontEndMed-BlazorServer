using MedicalCareWeb.Models.TipoCentro;
using System.ComponentModel.DataAnnotations;

namespace MedicalCareWeb.Models.TipoEspecialidad
{
    public class UpdateTipoEspecialidadRequestDto: TipoCentroDto
    {
        [Required]
        public int Id {  get; set; }
    }
}
