namespace Veros.Paperless.Model.Servicos.ReconhecimentoPorCampo
{
    using System;

    public interface IRelatorioEstatisticaDeReconhecimentoServico
    {
        RelatorioDeReconhecimentoPorCampo Obter(DateTime dataInicial, DateTime dataFinal);
    }
}
