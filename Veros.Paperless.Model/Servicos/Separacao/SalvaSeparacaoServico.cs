namespace Veros.Paperless.Model.Servicos.Separacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Ajustes;
    using Entidades;
    using ViewModel;
    using Repositorios;

    public class SalvaSeparacaoServico : ISalvaSeparacaoServico
    {
        private readonly IObtemLoteParaSeparacaoServico obtemLoteParaSeparacaoServico;
        private readonly IRessuscitaPaginaNaSeparacaoServico ressuscitaPaginaNaSeparacaoServico;
        private readonly IExcluiPaginaNaSeparacaoServico excluiPaginaNaSeparacaoServico;
        private readonly IReordenarDocumentosServico reordenarDocumentosServico;
        private readonly ICriaDocumentoNaSeparacaoServico criaDocumentoNaSeparacaoServico;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IRemoveDocumentosSemPaginaServico removeDocumentosSemPaginaServico;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IReclassificaDocumentoNaSeparacaoServico reclassificaDocumentoNaSeparacaoServico;
        private readonly IValidaSePodeSalvarAjustesServico validaSePodeSalvarAjustesServico;
        private readonly IRemovePdfSeparadoServico removePdfSeparadoServico;

        public SalvaSeparacaoServico(
            IObtemLoteParaSeparacaoServico obtemLoteParaSeparacaoServico, 
            IRessuscitaPaginaNaSeparacaoServico ressuscitaPaginaNaSeparacaoServico, 
            IExcluiPaginaNaSeparacaoServico excluiPaginaNaSeparacaoServico, 
            IReordenarDocumentosServico reordenarDocumentosServico, 
            ICriaDocumentoNaSeparacaoServico criaDocumentoNaSeparacaoServico, 
            IGravaLogDoLoteServico gravaLogDoLoteServico,
            ILoteRepositorio loteRepositorio, 
            IProcessoRepositorio processoRepositorio, 
            IRemoveDocumentosSemPaginaServico removeDocumentosSemPaginaServico, 
            IDocumentoRepositorio documentoRepositorio, 
            IReclassificaDocumentoNaSeparacaoServico reclassificaDocumentoNaSeparacaoServico, 
            IValidaSePodeSalvarAjustesServico validaSePodeSalvarAjustesServico, 
            IRemovePdfSeparadoServico removePdfSeparadoServico)
        {
            this.obtemLoteParaSeparacaoServico = obtemLoteParaSeparacaoServico;
            this.ressuscitaPaginaNaSeparacaoServico = ressuscitaPaginaNaSeparacaoServico;
            this.excluiPaginaNaSeparacaoServico = excluiPaginaNaSeparacaoServico;
            this.reordenarDocumentosServico = reordenarDocumentosServico;
            this.criaDocumentoNaSeparacaoServico = criaDocumentoNaSeparacaoServico;
            this.loteRepositorio = loteRepositorio;
            this.processoRepositorio = processoRepositorio;
            this.removeDocumentosSemPaginaServico = removeDocumentosSemPaginaServico;
            this.documentoRepositorio = documentoRepositorio;
            this.reclassificaDocumentoNaSeparacaoServico = reclassificaDocumentoNaSeparacaoServico;
            this.validaSePodeSalvarAjustesServico = validaSePodeSalvarAjustesServico;
            this.removePdfSeparadoServico = removePdfSeparadoServico;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
        }

        public void Executar(int loteId, int processoId, string textoDeAcoes)
        {
            this.validaSePodeSalvarAjustesServico.Validar(processoId);

            var acoes = AcaoDeSeparacao.MontarLista(textoDeAcoes);

            if (acoes.Any() == false)
            {
                return;
            }

            var loteParaSeparacao = this.obtemLoteParaSeparacaoServico.Executar(processoId);

            var acoesOrdenadas = acoes.OrderBy(x => x.Id);

            foreach (var acao in acoesOrdenadas)
            {
                if (this.IgnorarAcao(acao, acoes))
                {
                    continue;
                }

                this.ExecutarAcao(acao, loteParaSeparacao);
            }

            if (acoes.Any(x => x.Tipo == AcaoDeSeparacao.NovoDocumento))
            {
                this.reordenarDocumentosServico.Executar(loteParaSeparacao);    
            }

            this.removeDocumentosSemPaginaServico.Executar(loteParaSeparacao);
            this.documentoRepositorio.LimparFraudes(loteParaSeparacao.LoteId);
            this.documentoRepositorio.SetarExcluidoAjusteTemporario(loteId);
            this.removePdfSeparadoServico.Executar(loteParaSeparacao);

            this.loteRepositorio.AlterarStatus(loteId, LoteStatus.SetaReconhecimento);
            this.processoRepositorio.AlterarStatus(loteId, ProcessoStatus.AguardandoTransmissao);

            this.gravaLogDoLoteServico.Executar(LogLote.AcaoSeparacaoRealizada, loteId, "Lote foi salvo na separação");
        }

        private void ExecutarAcao(AcaoDeSeparacao acao, LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            switch (acao.Tipo)
            {
                case AcaoDeSeparacao.RessuscitarPagina:
                    this.ressuscitaPaginaNaSeparacaoServico.Executar(acao, loteParaSeparacao);
                    break;
                case AcaoDeSeparacao.ExcluirPagina:
                    this.excluiPaginaNaSeparacaoServico.Executar(acao, loteParaSeparacao);
                    break;
                case AcaoDeSeparacao.NovoDocumento:
                    this.criaDocumentoNaSeparacaoServico.Executar(acao, loteParaSeparacao);
                    break;
                case AcaoDeSeparacao.ReclassificarDocumento:
                    this.reclassificaDocumentoNaSeparacaoServico.Executar(acao, loteParaSeparacao);
                    break;
            }
        }

        private bool IgnorarAcao(AcaoDeSeparacao acao, IEnumerable<AcaoDeSeparacao> acoes)
        {
            switch (acao.Tipo)
            {
                case AcaoDeSeparacao.RessuscitarPagina:

                    return acoes.Any(x => 
                        x.PrimeiraPagina == acao.PrimeiraPagina && 
                        acao.Tipo == AcaoDeSeparacao.ExcluirPagina && 
                        x.Id > acao.Id);
                    
                case AcaoDeSeparacao.ExcluirPagina:

                    return acoes.Any(x =>
                        x.PrimeiraPagina == acao.PrimeiraPagina &&
                        acao.Tipo == AcaoDeSeparacao.RessuscitarPagina &&
                        x.Id > acao.Id);
            }
            
            return false;
        }
    }
}
