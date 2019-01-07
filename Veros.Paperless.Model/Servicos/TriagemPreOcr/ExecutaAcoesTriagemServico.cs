namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Modelo;
    using Repositorios;
    using Separacao;
    using ViewModel;

    public class ExecutaAcoesTriagemServico : IExecutaAcoesTriagemServico
    {
        private readonly IObtemLoteParaTriagemPreOcrServico obtemLoteParaTriagemPreOcrServico;
        private readonly RemoveDocumentosSemPaginaServico removeDocumentosSemPaginaServico;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IRessuscitaPaginaNaTriagemServico ressuscitaPaginaNaTriagemServico;
        private readonly IExcluiPaginaNaTriagemServico excluiPaginaNaTriagemServico;
        private readonly ICriaDocumentoNaTriagemServico criaDocumentoNaTriagemServico;
        private readonly IReclassificaDocumentoNaTriagemServico reclassificaDocumentoNaTriagemServico;
        private readonly MudaPaginaDeDocumentoNaTriagemServico mudaPaginaDeDocumentoNaTriagemServico;
        private readonly ReordenaPaginasTriagemServico reordenaPaginasTriagemServico;
        private readonly GravaGiroNaTriagemServico gravaGiroNaTriagemServico;

        public ExecutaAcoesTriagemServico(IObtemLoteParaTriagemPreOcrServico obtemLoteParaTriagemPreOcrServico,
            RemoveDocumentosSemPaginaServico removeDocumentosSemPaginaServico, 
            IDocumentoRepositorio documentoRepositorio, 
            IRessuscitaPaginaNaTriagemServico ressuscitaPaginaNaTriagemServico, 
            IExcluiPaginaNaTriagemServico excluiPaginaNaTriagemServico, 
            ICriaDocumentoNaTriagemServico criaDocumentoNaTriagemServico, 
            IReclassificaDocumentoNaTriagemServico reclassificaDocumentoNaTriagemServico, 
            MudaPaginaDeDocumentoNaTriagemServico mudaPaginaDeDocumentoNaTriagemServico, 
            ReordenaPaginasTriagemServico reordenaPaginasTriagemServico, 
            GravaGiroNaTriagemServico gravaGiroNaTriagemServico)
        {
            this.obtemLoteParaTriagemPreOcrServico = obtemLoteParaTriagemPreOcrServico;
            this.removeDocumentosSemPaginaServico = removeDocumentosSemPaginaServico;
            this.documentoRepositorio = documentoRepositorio;
            this.ressuscitaPaginaNaTriagemServico = ressuscitaPaginaNaTriagemServico;
            this.excluiPaginaNaTriagemServico = excluiPaginaNaTriagemServico;
            this.criaDocumentoNaTriagemServico = criaDocumentoNaTriagemServico;
            this.reclassificaDocumentoNaTriagemServico = reclassificaDocumentoNaTriagemServico;
            this.mudaPaginaDeDocumentoNaTriagemServico = mudaPaginaDeDocumentoNaTriagemServico;
            this.reordenaPaginasTriagemServico = reordenaPaginasTriagemServico;
            this.gravaGiroNaTriagemServico = gravaGiroNaTriagemServico;
        }

        public LoteTriagemViewModel ExecutarAcoes(int processoId, string textoDeAcoes, bool ignorarPaginasExcluidas, bool excluirDocumentosSemPaginas, string fase)
        {
            var acoes = AcaoDeTriagemPreOcr.MontarLista(textoDeAcoes);

            if (acoes.Any() == false)
            {
                return null;
            }

            var lote = this.obtemLoteParaTriagemPreOcrServico.Obter(processoId, ignorarPaginasExcluidas, fase);

            var acoesDiferenteDeGiro = acoes.Where(x => x.Tipo != AcaoDeTriagemPreOcr.Girar180 && x.Tipo != AcaoDeTriagemPreOcr.GirarAntiHorario && x.Tipo != AcaoDeTriagemPreOcr.GirarHorario).ToList();
            var acoesDeGiro = acoes.Where(x => x.Tipo == AcaoDeTriagemPreOcr.Girar180 || x.Tipo == AcaoDeTriagemPreOcr.GirarAntiHorario || x.Tipo == AcaoDeTriagemPreOcr.GirarHorario).ToList();

            this.ExecutaAcoes(acoesDiferenteDeGiro, lote);
            this.ExecutaAcoes(acoesDeGiro, lote);

            if (excluirDocumentosSemPaginas)
            {
                this.removeDocumentosSemPaginaServico.Executar(lote);
            }

            this.documentoRepositorio.LimparFraudes(lote.LoteId);

            return lote;
        }

        private void ExecutaAcoes(IList<AcaoDeTriagemPreOcr> acoes, LoteTriagemViewModel lote)
        {
            foreach (var acao in acoes)
            {
                if (this.IgnorarAcao(acao, acoes))
                {
                    continue;
                }

                this.ExecutarAcao(acao, lote);
            }
        }

        private void ExecutarAcao(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote)
        {
            switch (acao.Tipo)
            {
                case AcaoDeTriagemPreOcr.RessuscitarPagina:
                case AcaoDeTriagemPreOcr.RessuscitarFolha:
                    this.ressuscitaPaginaNaTriagemServico.Executar(acao, lote);
                    break;

                case AcaoDeTriagemPreOcr.ExcluirPagina:
                case AcaoDeTriagemPreOcr.ExcluirFolha:
                    this.excluiPaginaNaTriagemServico.Executar(acao, lote);
                    break;

                case AcaoDeTriagemPreOcr.NovoDocumento:
                    this.criaDocumentoNaTriagemServico.Executar(acao, lote);
                    break;

                case AcaoDeTriagemPreOcr.ReclassificarDocumento:
                    this.reclassificaDocumentoNaTriagemServico.Executar(acao, lote);
                    break;

                case AcaoDeTriagemPreOcr.MudarPaginaDeDocumento:
                    this.mudaPaginaDeDocumentoNaTriagemServico.Executar(acao, lote);
                    break;

                case AcaoDeTriagemPreOcr.ReordenarPaginas:
                    this.reordenaPaginasTriagemServico.Executar(acao, lote);
                    break;
        
                case AcaoDeTriagemPreOcr.GirarHorario:
                case AcaoDeTriagemPreOcr.GirarAntiHorario:
                case AcaoDeTriagemPreOcr.Girar180:
                    this.gravaGiroNaTriagemServico.Executar(acao, lote);
                    break;

                default:
                    throw new RegraDeNegocioException("Tipo de ação não esperada! #" + acao.Tipo);
            }
        }

        private bool IgnorarAcao(AcaoDeTriagemPreOcr acao, IEnumerable<AcaoDeTriagemPreOcr> acoes)
        {
            switch (acao.Tipo)
            {
                case AcaoDeTriagemPreOcr.RessuscitarPagina:

                    return acoes.Any(x =>
                        x.PrimeiraPagina == acao.PrimeiraPagina &&
                        x.Tipo == AcaoDeSeparacao.ExcluirPagina &&
                        x.Id > acao.Id);

                case AcaoDeTriagemPreOcr.ExcluirPagina:

                    return acoes.Any(x =>
                        x.PrimeiraPagina == acao.PrimeiraPagina &&
                        x.Tipo == AcaoDeSeparacao.RessuscitarPagina &&
                        x.Id > acao.Id);

                case AcaoDeTriagemPreOcr.MudarPaginaDeDocumento:
                    return acoes.Any(x =>
                        x.PrimeiraPagina == acao.PrimeiraPagina &&
                        x.Tipo == AcaoDeTriagemPreOcr.MudarPaginaDeDocumento &&
                        x.Id > acao.Id);
            }

            return false;
        }
    }
}
