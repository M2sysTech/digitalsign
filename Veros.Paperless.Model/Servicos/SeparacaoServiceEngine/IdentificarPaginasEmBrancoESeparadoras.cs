namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Entidades;
    using Framework;
    using Framework.Threads;
    using Repositorios;

    public class IdentificarPaginasEmBrancoESeparadoras : IIdentificarPaginasEmBrancoESeparadoras
    {
        private readonly IManipulaImagemServico manipulaImagemServico;
        private readonly IIdentificarPaginaSeparadora identificarPaginaSeparadora;
        private readonly ILocalizadorCarimboService localizadorCarimboService;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;

        public IdentificarPaginasEmBrancoESeparadoras(
            IManipulaImagemServico manipulaImagemServico, 
            IIdentificarPaginaSeparadora identificarPaginaSeparadora, 
            ILocalizadorCarimboService localizadorCarimboService, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico)
        {
            this.manipulaImagemServico = manipulaImagemServico;
            this.identificarPaginaSeparadora = identificarPaginaSeparadora;
            this.localizadorCarimboService = localizadorCarimboService;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
        }

        public List<ItemParaSeparacao> Executar(IList<ItemParaSeparacao> itensParaSeparacao)
        {
            var paralelismo = Paralelizar.Em(2);
#if DEBUG
            paralelismo = Paralelizar.Em(1);
#endif
            Parallel.For(0, itensParaSeparacao.Count, paralelismo, i =>
            {
                var pagina = itensParaSeparacao[i].Pagina;

                try
                {
                    itensParaSeparacao[i].CarregarBitmap();
                }
                catch (ImagemInvalidaException exception)
                {
                    this.gravaLogDaPaginaServico.ExecutarNovaThread(
                        LogPagina.AcaoPaginaErroSeparador, 
                        pagina.Id, 
                        pagina.Documento.Id, 
                        "Imagem corrompida ou outro problema");

                    throw new ImagemInvalidaException("Imagem corrompida ou outro problema");
                }

                Log.Application.DebugFormat("Processando arquivo #{0}", pagina.Id);

                if (pagina.Id == 137830)
                {
                    var asdf = 1;
                }

                try
                {
                    if (Contexto.SeparadorPorCarimbo)
                    {
                        ////var carimbo = new CarimboSeparador();
                        ////var tempoParaAnalisarCarimbo = MedirTempo.De(() => carimbo = this.localizadorCarimboService.ExecutarPorBitmaps(itensParaSeparacao[i]));
                        ////Log.Application.DebugFormat("Medição TEMPO analise de carimbo  ---- : {0}", tempoParaAnalisarCarimbo.TotalMilliseconds);

                        var carimbo = this.localizadorCarimboService.ExecutarPorBitmaps(itensParaSeparacao[i]);
                        
                        if (carimbo.Localizado)
                        {
                            pagina.Separadora = true;
                            pagina.AnaliseConteudoOcr = PaginaTipoOcr.TipoSeparadorCarimbo;
                            Log.Application.DebugFormat("Carimbo localizado na pagina #{0}", pagina.Id);
                        }
                    }

                    var imagemEmBranco = false;

                    var tempoParaVerificarBranco = MedirTempo.De(() => imagemEmBranco = this.manipulaImagemServico
                        .ImagemEmBranco(itensParaSeparacao[i], Contexto.MinWidthPixel, Contexto.MinHeightPixel, Contexto.MinMargemPixel));

                    Log.Application.DebugFormat("Tempo avaliação imagem branca/carimbo separador: {0}ms", tempoParaVerificarBranco.TotalMilliseconds);
                    if (imagemEmBranco)
                    {
                        pagina.EmBranco = true;
                        pagina.AnaliseConteudoOcr = PaginaTipoOcr.TipoBranco;
                    }
                    else
                    {
                        this.identificarPaginaSeparadora.Executar(itensParaSeparacao, itensParaSeparacao[i]);
                    }
                }
                catch (Exception exception)
                {
                    Log.Application.Error(exception);
                    throw;
                }

                ////dispose final do bitmap
                itensParaSeparacao[i].DescarregarBitmap();
            });

            return itensParaSeparacao as List<ItemParaSeparacao>;
        }
    }
}