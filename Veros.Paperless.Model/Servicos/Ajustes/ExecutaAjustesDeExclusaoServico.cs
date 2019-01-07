namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class ExecutaAjustesDeExclusaoServico : IExecutaAjustesDeExclusaoServico
    {
        private readonly IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public ExecutaAjustesDeExclusaoServico(
            IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio, 
            IPaginaRepositorio paginaRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.ajusteDeDocumentoRepositorio = ajusteDeDocumentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }
        
        public void Executar(IEnumerable<AjusteDeDocumento> ajustes)
        {
            foreach (var ajuste in ajustes.Where(x => x.Acao == AcaoAjusteDeDocumento.ExcluirPagina).OrderBy(x => x.Id))
            {
                this.paginaRepositorio.AlterarStatus(ajuste.Documento, ajuste.Pagina, PaginaStatus.StatusExcluida);

                this.gravaLogDoDocumentoServico.Executar(
                    LogDocumento.AcaoPaginaRemovidaNoAjuste, 
                    ajuste.Documento.Id, 
                    string.Format("Pagina {0} excluída no ajuste", ajuste.Pagina));

                this.ajusteDeDocumentoRepositorio.GravarComoProcessado(ajuste);
            }
        }
    }
}
