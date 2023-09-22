using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Validators.ValidatorDocument;

public class DocumentValidator 
{
    public bool validatorDocument(string padraDcoumento)
    {
        bool cpfValido = false;

        int dv1 = CalcularDigitoVerificador(padraDcoumento.Substring(0, 9));

        int dv2 = CalcularDigitoVerificador(padraDcoumento.Substring(0, 9) + dv1);

        // Passo 5: Compare os dígitos verificadores calculados com os dígitos originais
        if (dv1 == int.Parse(padraDcoumento[9].ToString()) && dv2 == int.Parse(padraDcoumento[10].ToString()))
        {
            cpfValido = true;
        }

        return cpfValido;
    }

    private static int CalcularDigitoVerificador(string parteCpf)
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

