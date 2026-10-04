namespace Desafio_dev_Target.Infraestrutura;

public static class EntradaConsole
{
    public static int? LerInteiroPositivo(string mensagem)
    {
        Console.Write(mensagem);

        if (!int.TryParse(Console.ReadLine(), out int numero) || numero <= 0)
        {
            Console.WriteLine("Informe um número inteiro positivo.");
            return null;
        }

        return numero;
    }
}