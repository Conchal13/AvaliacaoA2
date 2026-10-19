public class Folha
{
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public int Mes {get; set;}
    public int Ano {get; set;}
    public int HorasTrabalhadas {get; set;}
    public int ValorHora {get; set;}
    public float SalarioBruto {get; set;}
    public float ImpostoRenda {get; set;}
    public float Inss {get; set;}
    public float Fgts {get; set;}
    public float SalarioLiquido {get; set;}
}