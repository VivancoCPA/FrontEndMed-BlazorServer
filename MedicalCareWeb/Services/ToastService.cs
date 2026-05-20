using MedicalCareWeb.Common;

namespace MedicalCareWeb.Services;

// Services/ToastService.cs
public class ToastService
{
    public event Action<ToastNotification>? OnMostrar;

    public void Exito(string mensaje, int duracionMs = 4000) =>
        Mostrar(mensaje, ToastTipo.Exito, duracionMs);

    public void Error(string mensaje, int duracionMs = 5000) =>
        Mostrar(mensaje, ToastTipo.Error, duracionMs);

    public void Advertencia(string mensaje, int duracionMs = 4000) =>
        Mostrar(mensaje, ToastTipo.Advertencia, duracionMs);

    public void Info(string mensaje, int duracionMs = 4000) =>
        Mostrar(mensaje, ToastTipo.Info, duracionMs);

    // ✅ Muestra múltiples errores del backend uno por uno
    public void Errores(List<string> errores, int duracionMs = 5000)
    {
        foreach (var error in errores)
            Error(error, duracionMs);
    }

    private void Mostrar(string mensaje, ToastTipo tipo, int duracionMs)
    {
        OnMostrar?.Invoke(new ToastNotification
        {
            Mensaje = mensaje,
            Tipo = tipo,
            DuracionMs = duracionMs
        });
    }
}