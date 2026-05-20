namespace MedicalCareWeb.Models.Grupo;

public class GrupoDto
{
    public string NombreGrupo { get; set; } = string.Empty;

    public string Creador { get; set; } = string.Empty;

    public List<IntegranteDto> Integrantes { get; set; } = new();
}

public class IntegranteDto
{
    public string Nombre { get; set; } = string.Empty;

    public string AvatarUrl { get; set; } = string.Empty;
}