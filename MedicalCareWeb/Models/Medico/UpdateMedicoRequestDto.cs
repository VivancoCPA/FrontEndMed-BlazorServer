namespace MedicalCareWeb.Models.Medico
{
    public class UpdateMedicoRequestDto: CreateMedicoRequestDto
    {
        public Guid Id { get; set; }
    }
}
