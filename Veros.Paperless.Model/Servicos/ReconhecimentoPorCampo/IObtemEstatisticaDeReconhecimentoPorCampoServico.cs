namespace Veros.Paperless.Model.Servicos.ReconhecimentoPorCampo
{
    using System;

    public interface IObtemEstatisticaDeReconhecimentoPorCampoServico
    {
        RelatorioDeReconhecimentoPorCampo Obter(DateTime dataProducao);
    }
}