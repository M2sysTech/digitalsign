namespace Veros.Paperless.Model.Servicos.Suporte
{
    using System;
    using Entidades;
    using Framework;
    using Framework.Modelo;
    using Repositorios;

    public class RetornaTriagemServico : IRetornaTriagemServico
    {
        private readonly IProcedureRepositorio procedureRepositorio;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;

        public RetornaTriagemServico(IProcedureRepositorio procedureRepositorio, 
            IGravaLogDoLoteServico gravaLogDoLoteServico, 
            ILoteRepositorio loteRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaRepositorio paginaRepositorio, 
            IProcessoRepositorio processoRepositorio)
        {
            this.procedureRepositorio = procedureRepositorio;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.loteRepositorio = loteRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.processoRepositorio = processoRepositorio;
        }

        public void Executar(int loteId, string motivo)
        {
            var lote = this.loteRepositorio.ObterComPacote(loteId);
            if (lote == null)
            {
                throw new RegraDeNegocioException(string.Format("Dossiê (lote) [{0}] não encontrado.", loteId));   
            }

            if (lote.Status == LoteStatus.EmRecepcao || lote.Status == LoteStatus.EmTransmissao || lote.Status == LoteStatus.CapturaFinalizada ||
                lote.Status == LoteStatus.EmCaptura || lote.Status == LoteStatus.AguardandoRecaptura ||
                lote.Status == LoteStatus.AguardandoSeparacaoClassifier || lote.Status == LoteStatus.AguardandoTriagem ||
                lote.Status == LoteStatus.AguardandoControleQualidadeCef || lote.Status == LoteStatus.Faturamento)
            {
                throw new RegraDeNegocioException(
                    string.Format("Não é possível realizar a ação. Este Dossiê (lote) [{0}] está na fase: {1}.", 
                    loteId, 
                    lote.Status.DisplayName));
            }           

            try
            {
                this.paginaRepositorio.AlterarStatusDaPaginaNaoExcluidaPorLote(loteId, PaginaStatus.StatusTransmissaoOk);
                this.loteRepositorio.AlterarStatus(loteId, LoteStatus.AguardandoTriagem);
                this.documentoRepositorio.AlterarStatus(loteId, DocumentoStatus.TransmissaoOk);
                this.paginaRepositorio.ExcluirPaginasPorLote(loteId);   
                this.documentoRepositorio.ApagarVirtualPorLote(loteId);
                this.processoRepositorio.AlterarStatusPorLote(loteId, ProcessoStatus.AguardandoTriagem);
                this.gravaLogDoLoteServico.Executar(LogLote.AcaoPortalSuporte, loteId, string.Format("PORTAL SUPORTE: Lote setado para triagem - {0}", motivo));
            }
            catch (Exception exception)
            {
                Log.Application.Error("Erro ao executar retorno para triagem: ", exception);
                throw;
            }
        }
    }
}