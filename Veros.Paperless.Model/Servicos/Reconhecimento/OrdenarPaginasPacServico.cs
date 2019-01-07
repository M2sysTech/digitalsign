namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;
    using Image.Reconhecimento;
    using Repositorios;

    public class OrdenarPaginasPacServico : IOrdenarPaginasPacServico
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IBaixaArquivoFileTransferServico baixarArquivoFileTransfer;
        private readonly IReconheceImagem reconheceImagem;

        public OrdenarPaginasPacServico(
            IPaginaRepositorio paginaRepositorio, 
            IBaixaArquivoFileTransferServico baixarArquivoFileTransfer, 
            IReconheceImagem reconheceImagem)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.baixarArquivoFileTransfer = baixarArquivoFileTransfer;
            this.reconheceImagem = reconheceImagem;
        }

        public void Executar(IList<Pagina> paginas)
        {
            var imagemPacRecortada = this.baixarArquivoFileTransfer
                .BaixarPacCortada(paginas.First().Documento.Id);

            try
            {
                var resultadoReconhecimento = this.reconheceImagem
                    .Reconhecer(imagemPacRecortada, Image.TipoDocumentoReconhecivel.CabecalhoRodapePac);

                if (resultadoReconhecimento.NaoReconhecido)
                {
                    Log.Application.Error("Documento não terá páginas ordenadas. Nada foi reconhecido");
                    return;
                }

                var camposRecorte = this.ObterRecortesDoReconhecimento(resultadoReconhecimento);

                if (paginas.Count != camposRecorte.Count())
                {
                    Log.Application.Error("Documento não terá páginas ordenadas. Quantidade de recortes diferente da quantidade de paginas do documento");
                    return;
                }

                if (this.ExisteRecorteNulo(resultadoReconhecimento, camposRecorte))
                {
                    Log.Application.Error("Documento não terá páginas ordenadas. Existem recortes nulos");
                    return;
                }

                if (this.ExisteRecorteRepetido(resultadoReconhecimento, paginas, camposRecorte))
                {
                    Log.Application.Error("Documento não terá páginas ordenadas. Existem recortes com paginas repetidas");
                    return;
                }

                var paginasOrdenadasPorId = paginas.OrderBy(x => x.Id);

                for (var i = 0; i < paginasOrdenadasPorId.Count(); i++)
                {
                    var informacaoPagina = resultadoReconhecimento.Campos[camposRecorte.ElementAt(i)].Texto;

                    if (string.IsNullOrEmpty(informacaoPagina))
                    {
                        Log.Application.ErrorFormat("Reconhecimento trouxe informação da página nula. Recorte #{0}", camposRecorte.ElementAt(i));
                        continue;
                    }

                    var ordem = informacaoPagina.Trim().Substring(informacaoPagina.Length - 1, 1).ToInt();
                
                    Log.Application.InfoFormat(
                        "Página #{0} salva com ordem #{1}",
                        paginas[i].Id,
                        ordem);

                    paginasOrdenadasPorId.ElementAt(i).Ordem = ordem;
                    this.paginaRepositorio.SalvarOrdemDaPagina(paginasOrdenadasPorId.ElementAt(i).Id, ordem);
                }
            }
            catch (Exception exception)
            {
                Log.Application.Error(string.Format("Erro ao ordenar documento Mdoc #{0}", imagemPacRecortada), exception);
            }
        }

        private IEnumerable<string> ObterRecortesDoReconhecimento(ResultadoReconhecimento resultadoReconhecimento)
        {
            return resultadoReconhecimento
                .Campos
                .Keys
                .Where(x => x.Contains("recorte"));
        }

        private bool ExisteRecorteNulo(ResultadoReconhecimento resultadoReconhecimento, IEnumerable<string> camposRecorte)
        {
            for (var i = 0; i < camposRecorte.Count(); i++)
            {
                var informacaoPagina = resultadoReconhecimento.Campos[camposRecorte.ElementAt(i)].Texto;

                if (string.IsNullOrEmpty(informacaoPagina))
                {
                    Log.Application.ErrorFormat("Reconhecimento trouxe informação da página nula. Recorte #{0}", camposRecorte.ElementAt(i));
                    return true;
                }
            }

            return false;
        }

        private bool ExisteRecorteRepetido(ResultadoReconhecimento resultadoReconhecimento, IList<Pagina> paginas, IEnumerable<string> camposRecorte)
        {
            var ordenacao = new List<int>();

            for (var i = 0; i < paginas.Count(); i++)
            {
                var informacaoPagina = resultadoReconhecimento.Campos[camposRecorte.ElementAt(i)].Texto;
                ordenacao.Add(informacaoPagina.Substring(informacaoPagina.Length - 1, 1).ToInt());
            }

            return ordenacao.Distinct().Count() != paginas.Count();
        }
    }
}