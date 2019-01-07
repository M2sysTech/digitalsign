namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class AplicarAjusteDePaginaPorDocumento : IAplicarAjusteDePaginaPorDocumento
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IAplicarAjustesNaPaginaServico aplicarAjustesNaPagina;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;

        public AplicarAjusteDePaginaPorDocumento(
            IPaginaRepositorio paginaRepositorio, 
            IAplicarAjustesNaPaginaServico aplicarAjustesNaPagina, 
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.aplicarAjustesNaPagina = aplicarAjustesNaPagina;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
        }

        public bool Executar(int documentoId, IList<AjusteDeDocumento> ajustes)
        {
            var paginasDoDocumento = this.paginaRepositorio.ObterPorDocumentoId(documentoId);

            var temAjuste = false;

            foreach (var pagina in paginasDoDocumento)
            {
                var ajustesDaPagina = ajustes.Where(x => x.Pagina == pagina.Id);

                if (ajustesDaPagina.Any() == false)
                {
                    continue;
                }

                temAjuste = true;

                var imagemComAjustesAplicados = this.aplicarAjustesNaPagina.Executar(pagina, ajustesDaPagina.ToList());

                if (string.IsNullOrEmpty(imagemComAjustesAplicados) == false)
                {
                    this.postaArquivoFileTransferServico.PostarPagina(pagina, imagemComAjustesAplicados);

                    var extensao = Path.GetExtension(imagemComAjustesAplicados)
                        .Replace(".", string.Empty)
                        .ToUpper();

                    this.paginaRepositorio.AtualizarPaginaTipoDocumento(pagina.Id, extensao);
                    this.paginaRepositorio.AlterarStatusDaPagina(pagina.Id, PaginaStatus.StatusParaReconhecimentoPosAjuste);
                }
            }

            return temAjuste;
        }
    }
}