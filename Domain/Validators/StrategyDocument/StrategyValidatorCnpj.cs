namespace Domain.Validators.StrategyDocument;


public class StrategyValidatorCnpj : IStrategyValidatoDocument
{
    public bool IsValidator(string document)
    {
        bool cnpjValiator = false;

        int dv1 = CalcularDigitoVerificadorCnpj(document.Substring(0, 12));
        int dv2 = CalcularDigitoVerificadorCnpj(document.Substring(0, 12) + dv1);

        // Verifique se os dígitos verificadores calculados correspondem aos dígitos originais
        if (dv1 == int.Parse(document[12].ToString()) && dv2 == int.Parse(document[13].ToString()))
        {
            cnpjValiator = true;
        }

        return cnpjValiator;
    }

    private static int CalcularDigitoVerificadorCnpj(string parteCNPJ)
    {
        int soma = 0;
        int peso = 2;

        for (int i = parteCNPJ.Length - 1; i >= 0; i--)
        {
            int digito = int.Parse(parteCNPJ[i].ToString());
            soma += digito * peso;
            peso++;

            if (peso > 9)
            {
                peso = 2;
            }
        }

        int resto = soma % 11;
        var result = resto < 2 ? 0 : 11 - resto;

        return result;
    }
}

