namespace Desafio_dev_Target.Modelos;

public record Produto(int CodigoProduto, string DescricaoProduto, int Estoque)
{
    public int Estoque { get; set; } = Estoque;
}

public record DadosEstoque(List<Produto> Estoque);