namespace MedicalCareWeb.Common;

// Models/Common/ToastNotification.cs
public class ToastNotification
{
    public string Mensaje { get; set; } = string.Empty;
    public ToastTipo Tipo { get; set; } = ToastTipo.Info;
    public int DuracionMs { get; set; } = 4000;
    public Guid Id { get; set; } = Guid.NewGuid();
}

public enum ToastTipo
{
    Exito,
    Error,
    Advertencia,
    Info
}