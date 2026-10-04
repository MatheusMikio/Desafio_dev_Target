using System.Globalization;
using Desafio_dev_Target.Exercicios;

CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");

while (true)
{
    Console.WriteLine("\n=== Desafio ===");
    Console.WriteLine("1 - Calcular comissões de vendas");
    Console.WriteLine("2 - Movimentar estoque");
    Console.WriteLine("3 - Calcular juros por atraso");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha: ");

    switch (Console.ReadLine()?.Trim())
    {
        case "1": Comissoes.Executar(); break;
        case "2": Estoque.Executar(); break;
        case "3": Juros.Executar(); break;
        case "0": return;
        case null: return;
        default: Console.WriteLine("Opção inválida."); break;
    }
}