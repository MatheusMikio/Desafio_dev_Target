using Desafio_dev_Target.Infraestrutura;
using Desafio_dev_Target.Modelos;

namespace Desafio_dev_Target.Exercicios;

public static class Estoque
{
    private const string ArquivoEstado = "estoque-atual.json";

    public static void Executar()
    {
        var estado = File.Exists(ArquivosJson.Caminho(ArquivoEstado)) ? ArquivosJson.Ler<EstadoEstoque>(ArquivoEstado) : CriarEstadoInicial();

        if (estado?.Estoque is null || estado.Movimentacoes is null) return;

        Console.WriteLine("\nProdutos em estoque:");

        foreach (var produto in estado.Estoque) Console.WriteLine($"{produto.CodigoProduto} - {produto.DescricaoProduto}: {produto.Estoque}");

        int ? codigo = EntradaConsole.LerInteiroPositivo("Código do produto: ");

        if (codigo is null) return;

        var selecionado = estado.Estoque.FirstOrDefault(p => p.CodigoProduto == codigo);

        if (selecionado is null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        Console.Write("Tipo (E = entrada, S = saída): ");
        string ? tipo = Console.ReadLine()?.Trim().ToUpperInvariant();

        if (tipo is not ("E" or "S"))
        {
            Console.WriteLine("Tipo inválido.");
            return;
        }

        int ? quantidade = EntradaConsole.LerInteiroPositivo("Quantidade: ");

        if (quantidade is null) return;

        if (tipo == "S" && quantidade > selecionado.Estoque)
        {
            Console.WriteLine("Estoque insuficiente para a saída.");
            return;
        }

        int saldo;

        try
        {
            saldo = checked(selecionado.Estoque + (tipo == "E" ? quantidade.Value : -quantidade.Value));
        }
        catch (OverflowException)
        {
            Console.WriteLine("Quantidade excede o limite do estoque.");
            return;
        }

        int id = estado.Movimentacoes.Count == 0 ? 1 : checked(estado.Movimentacoes.Max(m => m.Id) + 1);
        selecionado.Estoque = saldo;
        estado.Movimentacoes.Add(new Movimentacao(id, selecionado.CodigoProduto,
            tipo == "E" ? "Entrada de mercadoria" : "Saída de mercadoria",
            quantidade.Value, saldo, DateTime.Now));

        if (ArquivosJson.Salvar(ArquivoEstado, estado)) Console.WriteLine($"Movimentação #{id} registrada. Estoque final de {selecionado.DescricaoProduto}: {saldo}");
    }

    private static EstadoEstoque ? CriarEstadoInicial()
    {
        var dados = ArquivosJson.Ler<DadosEstoque>("estoque.json");
        return dados?.Estoque is null ? null : new EstadoEstoque(dados.Estoque, []);
    }
}