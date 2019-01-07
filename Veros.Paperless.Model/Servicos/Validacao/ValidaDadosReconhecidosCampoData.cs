namespace Veros.Paperless.Model.Servicos.Validacao
{
    using Entidades;
    using Repositorios;

    public class ValidaDadosReconhecidosCampoData : IValidaDadosReconhecidosCampoData
    {
        private readonly ILogBatimentoRepositorio logBatimentoRepositorio;

        public ValidaDadosReconhecidosCampoData(ILogBatimentoRepositorio logBatimentoRepositorio)
        {
            this.logBatimentoRepositorio = logBatimentoRepositorio;
        }

        public bool PossuiMesNumerico(Indexacao indexacao)
        {
            var logBatimento = this.logBatimentoRepositorio.ObterPorIndexacao(indexacao);

            return logBatimento != null && CampoData.PossuiMesNumerico(logBatimento.ValorReconhecido);
        }
    }
}