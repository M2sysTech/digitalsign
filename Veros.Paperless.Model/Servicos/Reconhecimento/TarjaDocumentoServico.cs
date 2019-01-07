namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System;
    using System.Collections.Generic;
    using Image.Reconhecimento;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class TarjaDocumentoServico : ITarjaDocumentoServico
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly ITarjaRepositorio tarjaRepositorio;

        public TarjaDocumentoServico(IIndexacaoRepositorio indexacaoRepositorio, ITarjaRepositorio tarjaRepositorio)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.tarjaRepositorio = tarjaRepositorio;
        }

        public void Execute(Pagina pagina, ResultadoReconhecimento resultadoReconhecimento)
        {
            this.BuscarTarjaPorCampo(pagina, resultadoReconhecimento.Palavras, Campo.ReferenciaDeArquivoNomeTitular);
            if (this.DeveProcurarPaiOuMaeNesseTipoDocumento(pagina.Documento.TipoDocumento.Id))
            {
                this.BuscarTarjaPorCampo(pagina, resultadoReconhecimento.Palavras, Campo.ReferenciaDeArquivoNomePaiCliente);
                this.BuscarTarjaPorCampo(pagina, resultadoReconhecimento.Palavras, Campo.ReferenciaDeArquivoNomeMaeCliente);
            }
        }

        private bool DeveProcurarPaiOuMaeNesseTipoDocumento(int tipoDocumentoId)
        {
            var tiposAceitaveis = new List<int>()
            {
                TipoDocumento.CodigoComprovanteDeResidencia,
            };

            return tiposAceitaveis.IndexOf(tipoDocumentoId) >= 0;
        }

        private void BuscarTarjaPorCampo(Pagina pagina, IList<dynamic> palavrasReconhecidas, string campoRefArquivo)
        {
            IList<Indexacao> indexacoes;
            if (campoRefArquivo == Campo.ReferenciaDeArquivoNomeTitular)
            {
                indexacoes = this.indexacaoRepositorio.ObterPorReferenciaDeArquivo(pagina.Documento.Id, campoRefArquivo);
            }
            else
            {
                indexacoes = this.indexacaoRepositorio.ObterPorReferenciaArquivoDeUmDocumento(pagina.Documento.Lote.Id, campoRefArquivo);
            }

            var palavras = this.AdicionarPalavras(pagina, palavrasReconhecidas);

            if (indexacoes == null || indexacoes.Count < 1)
            {
                return;
            }

            var nomeAnterior = string.Empty;
            foreach (var indexacaoUnica in indexacoes)
            {
                var nomeEscolhido = indexacaoUnica.ValorFinal;
                if (string.IsNullOrEmpty(nomeEscolhido))
                {
                    nomeEscolhido = indexacaoUnica.SegundoValor;
                }

                if (string.IsNullOrEmpty(nomeEscolhido))
                {
                    continue;
                }

                nomeEscolhido = nomeEscolhido.Trim();
                if (nomeEscolhido == nomeAnterior)
                {
                    continue;
                }
                
                nomeAnterior = nomeEscolhido;
                var listaPalavras = nomeEscolhido.Split(Convert.ToChar(" "));
                var qtdeRetangulos = 0;
                var paginaAtual = 0;
                var posicoesRetangulos = string.Empty;

                foreach (var palavraUnica in listaPalavras)
                {
                    if (palavraUnica.Length <= 2)
                    {
                        continue;
                    }

                    foreach (var dynamic in palavras)
                    {
                        if (string.Equals(dynamic.Texto.ToLower(), palavraUnica.ToLower()))
                        {
                            qtdeRetangulos++;
                            posicoesRetangulos += string.Format("{0};{1};{2};{3}*", dynamic.Localizacao.Left, dynamic.Localizacao.Top, dynamic.Localizacao.Width, dynamic.Localizacao.Height);
                            paginaAtual = dynamic.Pagina.Id;
                            break;
                        }
                    }
                }

                if (qtdeRetangulos == 0)
                {
                    continue;
                }

                var tarja = new Tarja
                {
                    Documento = pagina.Documento,
                    Campo = indexacaoUnica.Campo,
                    PosicoesRetangulos = posicoesRetangulos.Substring(0, posicoesRetangulos.Length - 1),
                    QtdeRetangulos = qtdeRetangulos,
                    Pagina = new Pagina
                    {
                        Id = paginaAtual
                    }
                };

                this.tarjaRepositorio.Salvar(tarja);
            }
        }

        private IList<PalavraReconhecida> AdicionarPalavras(Pagina pagina, IList<dynamic> regioesTexto)
        {
            var palavrasReconhecidas = new List<PalavraReconhecida>();

            foreach (var dynamic in regioesTexto)
            {
                var palavraAtual = new PalavraReconhecida
                {
                    Texto = dynamic.Texto,
                    Altura = dynamic.Localizacao.Height,
                    Direita = dynamic.Localizacao.Right,
                    Esquerda = dynamic.Localizacao.Left,
                    Largura = dynamic.Localizacao.Width,
                    Topo = dynamic.Localizacao.Top,
                    Pagina = pagina
                };

                palavrasReconhecidas.Add(palavraAtual);
            }

            return palavrasReconhecidas;
        }
    }
}
