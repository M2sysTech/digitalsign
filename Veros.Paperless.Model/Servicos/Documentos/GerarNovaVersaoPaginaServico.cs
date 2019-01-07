namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Linq;
    using Entidades;
    using Framework;
    using Repositorios;

    public class GerarNovaVersaoPaginaServico : IGerarNovaVersaoPaginaServico
    {
        private const string AcaoAdicionarPrimeira = "P";
        private const string AcaoAdicionarUltima = "U";
        private const string AcaoAdicionarAntes = "AA";
        private const string AcaoAdicionarDepois = "AD";
        private const string AcaoSubstituir = "S";

        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGerarPaginasServico gerarPaginasServico;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public GerarNovaVersaoPaginaServico(
            IDocumentoRepositorio documentoRepositorio,
            IGerarPaginasServico gerarPaginasServico, 
            IPaginaRepositorio paginaRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gerarPaginasServico = gerarPaginasServico;
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(
            int documentoId, 
            int paginaId, 
            string acao, 
            string caminhoImagens, 
            string arquivos)
        {
            var documento = this.documentoRepositorio.ObterComPaginas(documentoId);

            var paginaBase = documento.Paginas.FirstOrDefault(x => x.Id == paginaId);

            var ordem = this.CalcularOrdemDaNova(documento, paginaBase, acao);

            var paginasNovas = this.gerarPaginasServico
                .AdicionarPaginas(documento, caminhoImagens, arquivos, ordem);

            if (acao == AcaoSubstituir)
            {
                var novaVersao = paginaBase.Versao.ToInt() + 1;

                foreach (var paginaNova in paginasNovas)
                {
                    paginaNova.PaginaPaiId = paginaBase.Id;
                    paginaNova.Versao = novaVersao.ToString();

                    this.gravaLogDoDocumentoServico.Executar(
                        LogDocumento.AcaoNovaVersaoDePagina, 
                        documentoId, 
                        "Nova versão para página [" + paginaNova.Ordem + "].");
                }
            }
            else
            {
                foreach (var paginaNova in paginasNovas)
                {
                    this.gravaLogDoDocumentoServico.Executar(
                        LogDocumento.AcaoAdicionaPagina, 
                        documentoId, 
                        "Nova página [" + paginaNova.Ordem + "] incluída no documento.");
                }
            }

            foreach (var pagina in documento.Paginas)
            {
                this.paginaRepositorio.Salvar(pagina);
            }
        }

        private int CalcularOrdemDaNova(Documento documento, Pagina paginaBase, string acao)
        {
            var ordem = documento.Paginas.Count + 1;

            switch (acao)
            {
                case AcaoAdicionarPrimeira:
                    foreach (var pagina in documento.ObterPaginasOrdenadas())
                    {
                        pagina.Ordem = pagina.Ordem + 1;
                    }

                    ordem = 0;
                    break;

                case AcaoAdicionarUltima:
                    return documento.Paginas.Count + 1;

                case AcaoAdicionarAntes:

                    ordem = paginaBase.Ordem;

                    foreach (var pagina in documento.ObterPaginasOrdenadas().Where(x => x.Ordem >= paginaBase.Ordem))
                    {
                        pagina.Ordem = pagina.Ordem + 1;
                    }

                    break;

                case AcaoAdicionarDepois:
                    ordem = paginaBase.Ordem + 1;

                    foreach (var pagina in documento.ObterPaginasOrdenadas().Where(x => x.Ordem > paginaBase.Ordem))
                    {
                        pagina.Ordem = pagina.Ordem + 1;
                    }

                    break;

                case AcaoSubstituir:
                    ordem = paginaBase.Ordem;
                    paginaBase.Status = PaginaStatus.StatusExcluida;
                    break;
            }

            return ordem;
        }

        private Pagina ObterPagina(Documento documento, int ordem)
        {
            var paginas = documento.ObterPaginasOrdenadas().ToArray();
            return paginas[ordem];
        }
    }
}
