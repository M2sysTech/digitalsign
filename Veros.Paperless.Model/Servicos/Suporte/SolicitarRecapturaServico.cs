namespace Veros.Paperless.Model.Servicos.Suporte
{
    using System;
    using Entidades;
    using Framework;
    using Framework.Modelo;
    using Repositorios;

    public class SolicitarRecapturaServico : ISolicitarRecapturaServico
    {
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;

        public SolicitarRecapturaServico(
            IGravaLogDoLoteServico gravaLogDoLoteServico, 
            ILoteRepositorio loteRepositorio, IProcessoRepositorio processoRepositorio)
        {
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.loteRepositorio = loteRepositorio;
            this.processoRepositorio = processoRepositorio;
        }

        public void Executar(int loteId, string motivo)
        {
            var processo = this.processoRepositorio.ObterPorLoteId(loteId);
            if (processo == null)
            {
                throw new RegraDeNegocioException(string.Format("Dossiê (Batch) [{0}] não encontrado.", loteId));   
            }

            if (processo.Lote.PacoteProcessado == null)
            {
                throw new RegraDeNegocioException(string.Format("Este Dossiê (Batch) [{0}] não está associado a um movimento.", loteId));
            }

            if (processo.Lote.PacoteProcessado.StatusPacote == StatusPacote.AprovadoNaQualidade || processo.Lote.ResultadoQualidadeCef == "A")
            {
                throw new RegraDeNegocioException(string.Format("Este Dossiê (Batch) [{0}] já foi aprovado pela CEF.", loteId));
            }

            if (processo.Lote.LoteCef != null && processo.Lote.LoteCef.Status == LoteCefStatus.AprovadoNaQualidade)
            {
                throw new RegraDeNegocioException(string.Format("Este Dossiê (Batch) [{0}] já foi aprovado pela CEF.", loteId));
            }

            try
            {
                this.loteRepositorio.SetarParaRecaptura(loteId, motivo);
                this.processoRepositorio.AlterarStatusPorLote(loteId, ProcessoStatus.AguardandoRecaptura);
                this.gravaLogDoLoteServico.Executar(LogLote.AcaoPortalSuporte, 
                    loteId, 
                    string.Format("PORTAL SUPORTE: Lote setado para recaptura - {0}", motivo));
            }
            catch (Exception exception)
            {
                Log.Application.Error(string.Format("Erro ao setar lote [{0}] para recaptura : ", processo.Lote.Id), exception);
                throw;
            }
        }
    }
}
