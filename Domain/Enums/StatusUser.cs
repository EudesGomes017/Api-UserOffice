using System.ComponentModel;

namespace Domain.Enums;
public enum StatusUser
{
    [Description("Pessoa Fisíca")]
    Física = 1,

    [Description("Pessoa Jurídica")]
    Jurídica = 2,

    [Description("Administrador")]
    Administrador = 3,

    [Description("Usuário")]
    Usuário = 4,

    [Description("Colaborador")]
    Colaborador = 5,
}
