namespace Veros.Paperless.Model.Consultas
{
    using Entidades;

    public interface IRelatorioFaturamentoConsulta
    {
        RelatorioFaturamento Obter(string dataInicio, string dataFim);
    }
}