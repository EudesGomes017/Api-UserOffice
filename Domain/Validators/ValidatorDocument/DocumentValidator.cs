using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Validators.ValidatorDocument;

public class DocumentValidator
{
    //cpf
    public bool validatorDocumentCpf(string padraDcoumento)
    {
        bool cpfValido = false;

        int dv1 = CalcularDigitoVerificadorCpf(padraDcoumento.Substring(0, 9));

        int dv2 = CalcularDigitoVerificadorCpf(padraDcoumento.Substring(0, 9) + dv1);

        // Passo 5: Compare os dígitos verificadores calculados com os dígitos originais
        if (dv1 == int.Parse(padraDcoumento[9].ToString()) && dv2 == int.Parse(padraDcoumento[10].ToString()))
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


    public bool validatorDocumentCnpj(string padraDcoumento)
    {
        bool cnpjValiator = false;

        int dv1 = CalcularDigitoVerificadorCnpj(padraDcoumento.Substring(0, 12));
        int dv2 = CalcularDigitoVerificadorCnpj(padraDcoumento.Substring(0, 12) + dv1);

        // Verifique se os dígitos verificadores calculados correspondem aos dígitos originais
        if (dv1 == int.Parse(padraDcoumento[12].ToString()) && dv2 == int.Parse(padraDcoumento[13].ToString()))
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
        return resto < 2 ? 0 : 11 - resto;
    }
}

