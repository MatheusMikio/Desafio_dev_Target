namespace Desafio_dev_Target.Modelos;

public record Movimentacao(int Id, int CodigoProduto, string Descricao, int Quantidade, int EstoqueFinal, DateTime Data);
public record EstadoEstoque(List<Produto> Estoque, List<Movimentacao> Movimentacoes);