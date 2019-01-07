namespace Veros.Paperless.Model.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IFaturamentoRepositorio : IRepositorio<Faturamento>
    {
        IList<Faturamento> ObterPedenciasDeFaturamentoPorArquivoParaDataReferencia(DateTime dataParaFaturamento );

        IList<Faturamento> ObterPorPeriodoETipo(int tipoFaturamento, DateTime dataInicio, DateTime dataFim);

        IList<Faturamento> ObterFaturamentoDiarioOk(DateTime dataParaFaturamento);

        IList<Faturamento> ObterPendenciasDeFaturamentoPorPacote();

        IList<Faturamento> ObterPendenciasDeFaturamentoMensal();
        
        Faturamento ObterUmFaturamentoDiarioPendente(DateTime dataFaturamento);

        IList<Faturamento> ObterDatasPendentesDeFaturamentoPorTipo(int tipoFaturamento);
        
        IList<Faturamento> ObterTodosPorTipoParaDataReferencia(int tipoFaturamentoDiario, DateTime dataParaFaturamento);
        
        Faturamento ObterUmFaturamentoMensalPendente(DateTime dataDeFaturamento);
        
        IList<Faturamento> ObterPendenciasDeFaturamentoPorDiaParaDataReferencia(DateTime dataParaFaturamento);
        
        Faturamento ObterUmFaturamentoArquivoPendentePorNome(string nomeArquivo);

        IList<Faturamento> ObterFaturamentoPorArquivoOk(DateTime dataDeFaturamento);
    }
}
