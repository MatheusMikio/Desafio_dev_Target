using System.Globalization;

namespace Desafio_dev_Target.Exercicios;

public static class Juros
{
    public static void Executar()
    {
        Console.Write("Valor original (R$): ");
        string ? entrada = Console.ReadLine();
        const NumberStyles formato = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

        if (entrada is null || 
            !(decimal.TryParse(entrada, formato, CultureInfo.CurrentCulture, out decimal valor) ||
              decimal.TryParse(entrada, formato, CultureInfo.InvariantCulture, out valor)) || valor < 0)
        {
            Console.WriteLine("Valor inválido.");
            return;
        }

        Console.Write("Data de vencimento (dd/MM/aaaa): ");
        if (!DateOnly.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out var vencimento))
        {
            Console.WriteLine("Data inválida.");
            return;
        }

        int diasAtraso = Math.Max(0, DateOnly.FromDateTime(DateTime.Today).DayNumber - vencimento.DayNumber);
        // Juros simples: 2,5% do valor original por dia de atraso.
        decimal juros = decimal.Round(valor * 0.025m * diasAtraso, 2, MidpointRounding.AwayFromZero);
        Console.WriteLine($"Dias de atraso: {diasAtraso}");
        Console.WriteLine($"Juros: {juros:C}");
        Console.WriteLine($"Total a pagar: {valor + juros:C}");
    }
}