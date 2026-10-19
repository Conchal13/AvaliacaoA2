using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();
AppDbContext context = new AppDbContext();

app.MapGet("/", () => "Avaliação A2 - 2, Alunos: Pedro Volpatto, Lucas Aliel!");

app.MapPost("/api/folha/cadastrar", async ([FromServices] AppDbContext context, [FromBody] CriarFolhaDto request) =>
{
    try
    {
        if(request.Mes < 1 || request.Mes > 12)
        {
            return Results.BadRequest("Mês deve estar entre 1 e 12");

        }
        if(request.Ano < 2000)
        {
            return Results.BadRequest("Ano deve ser maior ou igual a 2000");
            
        }

        var folhaExistente = await context.Folhas.Where(f=> f.Cpf == request.Cpf && f.Mes == request.Mes && f.Ano == request.Ano).FirstOrDefaultAsync();
        if(folhaExistente  != null)
        {
            return Results.BadRequest("Folha deste CPF/mês e ano já cadastrada");
        }

        var salarioBruto = request.HorasTrabalhadas * request.ValorHora;

        var impostoRenda = Calculos.CalcularImpostoRenda(salarioBruto);

        var inss = Calculos.CalcularInss(salarioBruto);

        var fgts = Calculos.CalcularFgts(salarioBruto);

        var salarioLiquido = Calculos.CalcularSalarioLiquido(salarioBruto, impostoRenda, inss);

        var folha = new Folha();
        folha.Nome = request.Nome;
        folha.Cpf = request.Cpf;
        folha.Mes = request.Mes;
        folha.Ano = request.Ano;
        folha.HorasTrabalhadas = request.HorasTrabalhadas;
        folha.ValorHora = request.ValorHora;
        folha.SalarioBruto = salarioBruto;
        folha.ImpostoRenda = impostoRenda;
        folha.Inss = inss;
        folha.SalarioLiquido = salarioLiquido;
        folha.Fgts = fgts;
        
        await context.AddAsync(folha);
        await context.SaveChangesAsync();

        return Results.Created("", folha);
    }
    catch (Exception)
    {
        return Results.Text("Erro Desconhecido", statusCode: 500);
    }
});

app.MapGet("/api/folha/listar", async ([FromServices] AppDbContext context) =>
{   
    var folhas = await context.Folhas.ToListAsync();
    if(folhas.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Ok(folhas);
});

app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", async ([FromServices] AppDbContext context, string cpf, int mes, int ano) =>
{
    var folha = await context.Folhas.Where(f => f.Cpf == cpf && f.Mes == mes && f.Ano == ano).FirstOrDefaultAsync();

    if(folha == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(folha);
});

app.MapDelete("/api/folha/remover/{cpf}/{mes}/{ano}", async ([FromServices] AppDbContext context, string cpf, int mes, int ano) =>
{
    var folha = await context.Folhas.Where(f => f.Cpf == cpf && f.Mes == mes && f.Ano == ano).FirstOrDefaultAsync();
    
    if(folha == null)
    {
        return Results.NotFound();
    }

    context.Folhas.Remove(folha);
    await context.SaveChangesAsync();

    return Results.Ok("Folha excluida com sucesso");
});

app.MapGet("/api/folha/total-liquido", async ([FromServices] AppDbContext context) =>
{
    var folhas = await context.Folhas.ToListAsync();

    if(folhas.Count == 0)
    {
        return Results.NotFound();
    }

    var totalLiquido = folhas.Select(f => f.SalarioLiquido).Sum();

    return Results.Ok(float.Round(totalLiquido, 2));
});

app.Run();
