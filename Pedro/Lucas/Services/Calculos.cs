public static class Calculos
{
    public static float CalcularImpostoRenda(float salarioBruto)
    {
        if (salarioBruto > 4664.68)
        {
            return (salarioBruto * 0.275F) - 869.36F;
        }
        else if (salarioBruto >= 3751.06)
        {
            return (salarioBruto * 0.225F) - 636.13F;
        }
        else if (salarioBruto >= 2826.66)
        {
            return (salarioBruto * 0.15F) - 354.80F;
        }
        else if (salarioBruto >= 1903.99)
        {
            return (salarioBruto * 0.075F) - 142.80F;
        }
        else
        {
            return 0;
        }
    }

    public static float CalcularInss(float salarioBruto)
    {
        if(salarioBruto > 5645.81)
        {
            return 621.03F;
        }else if (salarioBruto >= 2822.91)
        {
            return salarioBruto * 0.11F;
        }else if (salarioBruto >= 1693.73)
        {
            return salarioBruto * 0.09F;
        }
        else
        {
            return salarioBruto * 0.08F;
        }
    }

    public static float CalcularFgts(float salarioBruto)
    {
        return salarioBruto * 0.08F;
    }

    public static float CalcularSalarioLiquido(float salarioBruto, float impostoRenda, float inss)
    {
        return salarioBruto - impostoRenda - inss;
    }
}