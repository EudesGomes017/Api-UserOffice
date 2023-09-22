using Domain.Validators.StrategyDocument.Interface;

namespace Domain.Validators.StrategyDocument;

public class StrategyValidatorCpf : IStrattegyValidatoDocument
{
    public bool IsValidator(string document)
    {
        bool cpfValido = false;

        int dv1 = CalcularDigitoVerificadorCpf(document.Substring(0, 9));

        int dv2 = CalcularDigitoVerificadorCpf(document.Substring(0, 9) + dv1);

        // Passo 5: Compare os dígitos verificadores calculados com os dígitos originais
        if (dv1 == int.Parse(document[9].ToString()) && dv2 == int.Parse(document[10].ToString()))
        {
            cpfValido = true;
        }

        return cpfValido;
    }

    //cpf
    private static int CalcularDigitoVerificadorCpf(string parteCpf)
    {
        int soma = 0;
        for (int i = 0; i < parteCpf.Length; i++)
        {
            int digito = int.Parse(parteCpf[i].ToString());
            soma += digito * (parteCpf.Length + 1 - i);
        }
        int resto = soma % 11;

        var result = resto < 2 ? 0 : 11 - resto;

        return result;
    }
}

