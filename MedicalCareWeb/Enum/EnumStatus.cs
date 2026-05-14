using System.ComponentModel.DataAnnotations;

namespace MedicalCareWeb.Enum;

public enum EnumStatus
{
    [Display(Name = "Activo")]
    Activo = 1,

    [Display(Name = "Inactivo")]
    Inactivo = 0
}
