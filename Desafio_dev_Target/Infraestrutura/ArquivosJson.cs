using System.Text.Json;

namespace Desafio_dev_Target.Infraestrutura;

public static class ArquivosJson
{
    private static readonly JsonSerializerOptions Opcoes = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public static string Caminho(string arquivo) => Path.Combine(AppContext.BaseDirectory, arquivo);

    public static T ? Ler<T>(string arquivo)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(File.ReadAllText(Caminho(arquivo)), Opcoes);
        }
        catch (Exception erro) when (erro is IOException or JsonException or UnauthorizedAccessException)
        {
            Console.WriteLine($"Não foi possível ler {arquivo}: {erro.Message}");
            return default;
        }
    }

    public static bool Salvar<T>(string arquivo, T dados)
    {
        string destino = Caminho(arquivo);
        string temporario = destino + ".tmp";
        try
        {
            File.WriteAllText(temporario, JsonSerializer.Serialize(dados, Opcoes));
            File.Move(temporario, destino, true);
            return true;
        }
        catch (Exception erro) when (erro is IOException or UnauthorizedAccessException)
        {
            Console.WriteLine($"Não foi possível salvar {arquivo}: {erro.Message}");
            return false;
        }
    }
}