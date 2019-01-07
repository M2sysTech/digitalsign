namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Entidades;
    using ImagemOriginal;
    using Repositorios;

    public class AplicarAjustesNaPaginaServico : IAplicarAjustesNaPaginaServico
    {
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IRotacionarImagemServico rotacionarImagemServico;
        private readonly IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio;
        private readonly IAplicarAjusteDeExclusaoDePagina aplicarAjusteDeExclusaoDePagina;
        private readonly IObtemPaginaOriginalServico obtemPaginaOriginalServico;

        public AplicarAjustesNaPaginaServico(
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IRotacionarImagemServico rotacionarImagemServico, 
            IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio, 
            IAplicarAjusteDeExclusaoDePagina aplicarAjusteDeExclusaoDePagina, 
            IObtemPaginaOriginalServico obtemPaginaOriginalServico)
        {
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.rotacionarImagemServico = rotacionarImagemServico;
            this.ajusteDeDocumentoRepositorio = ajusteDeDocumentoRepositorio;
            this.aplicarAjusteDeExclusaoDePagina = aplicarAjusteDeExclusaoDePagina;
            this.obtemPaginaOriginalServico = obtemPaginaOriginalServico;
        }

        public string Executar(Pagina pagina, IList<AjusteDeDocumento> ajustesDaPagina)
        {
            string imagemAjustada = null;
            
            var paginaParaAjuste = this.ObterCaminhoDoArquivo(pagina, ajustesDaPagina);

            if (ajustesDaPagina.Any(x => x.Acao == AcaoAjusteDeDocumento.ExcluirPagina))
            {
                this.aplicarAjusteDeExclusaoDePagina.Executar(pagina);
                this.ajusteDeDocumentoRepositorio.GravarTodosComoProcessado(pagina);

                return string.Empty;
            }

            foreach (var ajuste in ajustesDaPagina)
            {
                if (ajuste.Acao == AcaoAjusteDeDocumento.UsarOriginal)
                {
                    var paginaOriginal = this.obtemPaginaOriginalServico.Executar(pagina.Id);

                    var ajustesDoAvo = this.ajusteDeDocumentoRepositorio.ObterPendentesPorPagina(paginaOriginal.Id);
                    if (ajustesDoAvo.Any(x => x.Acao == AcaoAjusteDeDocumento.ExcluirPagina))
                    {
                        this.aplicarAjusteDeExclusaoDePagina.Executar(pagina);
                        this.ajusteDeDocumentoRepositorio.GravarTodosComoProcessado(pagina);
                        this.ajusteDeDocumentoRepositorio.GravarTodosComoProcessado(paginaOriginal);

                        return string.Empty;
                    }

                    var caminhoArquivoPagina = Path.Combine(
                        ConfiguracaoAjusteImagem.CaminhoPadrao,
                        string.Format("{0:000000000}.{1}", paginaOriginal.Id, paginaOriginal.TipoArquivo));

                    this.baixaArquivoFileTransferServico.BaixarArquivoNaPasta(
                        paginaOriginal.Id, 
                        paginaOriginal.TipoArquivo, 
                        caminhoArquivoPagina, 
                        false, 
                        paginaOriginal.DataCenter);

                    this.ajusteDeDocumentoRepositorio.GravarComoProcessado(ajuste);

                    paginaParaAjuste.CaminhoArquivo = caminhoArquivoPagina;
                    paginaParaAjuste.Pagina = paginaOriginal;
                    paginaParaAjuste.AjusteNaPaginaOriginal = true;

                    imagemAjustada = this.Executar(paginaOriginal, ajustesDoAvo);
                }

                if (ajuste.Acao == AcaoAjusteDeDocumento.GirarHorario)
                {
                    imagemAjustada = this.rotacionarImagemServico.Executar(paginaParaAjuste.CaminhoArquivo, -90);
                }

                if (ajuste.Acao == AcaoAjusteDeDocumento.GirarAntiHorario)
                {
                    imagemAjustada = this.rotacionarImagemServico.Executar(paginaParaAjuste.CaminhoArquivo, 90);
                }

                if (ajuste.Acao == AcaoAjusteDeDocumento.Girar180)
                {
                    imagemAjustada = this.rotacionarImagemServico.Executar(paginaParaAjuste.CaminhoArquivo, 180);
                }

                paginaParaAjuste.CaminhoArquivo = imagemAjustada;
                this.ajusteDeDocumentoRepositorio.GravarComoProcessado(ajuste);
            }

            return paginaParaAjuste.CaminhoArquivo;
        }

        private PaginaParaAjuste ObterCaminhoDoArquivo(Pagina pagina, IEnumerable<AjusteDeDocumento> ajustesDaPagina)
        {
            string caminhoArquivoPagina = null;
            var paginaParaAjuste = new PaginaParaAjuste();

            caminhoArquivoPagina = Path.Combine(
                ConfiguracaoAjusteImagem.CaminhoPadrao, 
                string.Format("{0:000000000}.{1}", pagina.Id, pagina.TipoArquivo));

            this.baixaArquivoFileTransferServico
                .BaixarArquivoNaPasta(pagina.Id, pagina.TipoArquivo, caminhoArquivoPagina, false, pagina.DataCenter);

            paginaParaAjuste.CaminhoArquivo = caminhoArquivoPagina;
            paginaParaAjuste.Pagina = pagina;

            return paginaParaAjuste;
        }

        private class PaginaParaAjuste
        {
            public string CaminhoArquivo
            {
                get;
                set;
            }

            public Pagina Pagina
            {
                get;
                set;
            }

            public bool AjusteNaPaginaOriginal
            {
                get;
                set;
            }
        }
    }
}