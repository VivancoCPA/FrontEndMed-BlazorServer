using System.ComponentModel.DataAnnotations;

namespace MedicalCareWeb.Models.Centro
{
    public class CentroDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int TypeId { get; set; } 
        public string? TypeName { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime createdAt { get; set; }
    }
}
