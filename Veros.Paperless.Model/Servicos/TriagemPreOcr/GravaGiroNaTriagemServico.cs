namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using System;
    using Entidades;
    using Framework.Servicos;
    using Repositorios;
    using ViewModel;

    public class GravaGiroNaTriagemServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private readonly IPaginaRepositorio paginaRepositorio;

        public GravaGiroNaTriagemServico(ISessaoDoUsuario userSession, 
            IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico, 
            IPaginaRepositorio paginaRepositorio)
        {
            this.userSession = userSession;
            this.ajusteDeDocumentoRepositorio = ajusteDeDocumentoRepositorio;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
            this.paginaRepositorio = paginaRepositorio;
        }

        public void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote)
        {
            foreach (var paginaId in acao.Paginas)
            {
                this.GravarAjusteDeGiro(acao, paginaId, lote);
            }
        }

        private void GravarAjusteDeGiro(AcaoDeTriagemPreOcr acao, int paginaId, LoteTriagemViewModel lote)
        {
            var documento = this.ObterDocumento(paginaId, lote);

            var ajuste = new AjusteDeDocumento
            {
                Acao = AcaoAjusteDeDocumento.FromValue(acao.Tipo),
                Documento = documento,
                Status = AjusteDeDocumento.SituacaoAberto,
                DataRegistro = DateTime.Now,
                UsuarioCadastro = (Usuario)this.userSession.UsuarioAtual,
                Pagina = paginaId
            };

            this.ajusteDeDocumentoRepositorio.Salvar(ajuste);

            this.gravaLogDaPaginaServico.Executar(LogPagina.AcaoGiroManual,
                        paginaId, 
                        documento.Id,
                        string.Format("Giro manual [{0}] [{1}]", acao.Tipo, lote.Fase));

            lote.MarcaDocumentoManipulado(documento.Id);
        }

        private Documento ObterDocumento(int paginaId, LoteTriagemViewModel lote)
        {
            var documentoViewModel = lote.ObterDocumentoDaPagina(paginaId);

            if (documentoViewModel != null && documentoViewModel.Id > 0)
            {
                return new Documento { Id = documentoViewModel.Id };
            }

            var pagina = this.paginaRepositorio.ObterPorId(paginaId);

            if (pagina != null && pagina.Documento != null)
            {
                return pagina.Documento;
            }

            return new Documento { Id = 0 };
        }
    }
}
