using System.ComponentModel;

namespace Domain.Enums;
public enum StatusUser
{
    [Description("Pessoa Fisíca")]
    Fisica = 1,

    [Description("Pessoa Jurídica")]
    Juridica = 2,

    [Description("Administrador")]
    Administrador = 3,

    [Description("Usuário")]
    Usuario = 4,

    [Description("Colaborador")]
    Colaborador = 5,
}
