namespace MedicalCareWeb.Common;

public record ArchivoDTO(string Nombre, string ContentType, Func<Stream> AbrirStream);