namespace Desafio_dev_Target.Modelos;

public record Venda(string Vendedor, decimal Valor);
public record DadosVendas(List<Venda> Vendas);