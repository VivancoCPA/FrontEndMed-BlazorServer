using MedicalCareWeb.Common;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MedicalCareWeb.Models.Aseguradora;

public class UpdateAseguradoraRequestDto: CreateAseguradoraRequestDto
{
    public Guid Id { get; set; }

}
