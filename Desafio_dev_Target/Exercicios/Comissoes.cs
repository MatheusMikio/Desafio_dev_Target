using Desafio_dev_Target.Infraestrutura;
using Desafio_dev_Target.Modelos;

namespace Desafio_dev_Target.Exercicios;

public static class Comissoes
{
    public static void Executar()
    {
        var dados = ArquivosJson.Ler<DadosVendas>("vendas.json");

        if (dados?.Vendas is null) return;

        Console.WriteLine("\nComissão por vendedor:");
        foreach (var venda in dados.Vendas.GroupBy(v => v.Vendedor))
        {
            decimal comissao = venda.Sum(v => CalcularPorVenda(v.Valor));
            Console.WriteLine($"{venda.Key}: {comissao:C}");
        }
    }

    private static decimal CalcularPorVenda(decimal valor)
    {
        decimal percentual = valor < 100m ? 0m : valor < 500m ? 0.01m : 0.05m;
        return decimal.Round(valor * percentual, 2, MidpointRounding.AwayFromZero);
    }
}