namespace Veros.Paperless.Model.Servicos.Classificacao
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using Entidades;
    using Framework;
    using Framework.Modelo;
    using Repositorios;
    using SeparacaoServiceEngine;
    using TrataImagem;

    public class SepararDocumentos : ISepararDocumentos
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IIdentificarPaginasEmBrancoESeparadoras identificarPaginasEmBrancoESeparadoras;
        private readonly IDividirPaginasPorPaginaSeparadora dividirPaginasPorPaginaSeparadora;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IGerarThumbnailServico gerarThumbnailServico;
        private readonly IIdentificaTipoDocumentoServico identificaTipoDocumentoServico;
        private readonly ICorrigeOrientacaoServico corrigeOrientacaoServico;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private readonly IValidacaoImagem validacaoImagem;

        public SepararDocumentos(
            IPaginaRepositorio paginaRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            IIdentificarPaginasEmBrancoESeparadoras identificarPaginasEmBrancoESeparadoras, 
            IDividirPaginasPorPaginaSeparadora dividirPaginasPorPaginaSeparadora, 
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IGerarThumbnailServico gerarThumbnailServico, 
            IIdentificaTipoDocumentoServico identificaTipoDocumentoServico, 
            ICorrigeOrientacaoServico corrigeOrientacaoServico, 
            IIndexacaoRepositorio indexacaoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico,
            IValidacaoImagem validacaoImagem)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.identificarPaginasEmBrancoESeparadoras = identificarPaginasEmBrancoESeparadoras;
            this.dividirPaginasPorPaginaSeparadora = dividirPaginasPorPaginaSeparadora;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.gerarThumbnailServico = gerarThumbnailServico;
            this.identificaTipoDocumentoServico = identificaTipoDocumentoServico;
            this.corrigeOrientacaoServico = corrigeOrientacaoServico;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
            this.validacaoImagem = validacaoImagem;
        }

        public List<ItemParaSeparacao> Executar(int processoId)
        {
            var paginas = this.paginaRepositorio.ObterPorProcesso(processoId);
            var documentos = this.documentoRepositorio.ObterTodosPorProcesso(new Processo { Id = processoId });

            if (documentos.Any() == false)
            {
                throw new RegraDeNegocioException("Separacao do processo #" + processoId + ". ");
            }

            var cpf = documentos.First().Cpf;
            var lote = documentos.First().Lote;
            var processo = documentos.First().Processo;
            
            var documento = documentos.FirstOrDefault(x => 
                x.TipoDocumento.Id == TipoDocumento.CodigoDocumentoGeral && 
                x.Status != DocumentoStatus.Excluido);

            var versaoAtual = documento == null ? "0" : documento.Versao;

            var ordem = 2;
            
            var paginasJpeg = paginas
                .Where(x => x.TipoArquivo != "PDF")
                .OrderBy(x => x.Ordem)
                .ToList();

            List<ItemParaSeparacao> arquivosParaProcessar = null;

            var tempoParaBaixarELimparImagens = MedirTempo.De(() => 
                arquivosParaProcessar = this.ObterArquivosParaProcessar(paginasJpeg));

            Log.Application.DebugFormat("Tempo para baixar {0}s", tempoParaBaixarELimparImagens.TotalSeconds);

            var itensComBrancosIdentificados = this.identificarPaginasEmBrancoESeparadoras.Executar(arquivosParaProcessar);

            var paginasDivididasPorSeparadora = this.dividirPaginasPorPaginaSeparadora.Executar(itensComBrancosIdentificados);

            if (Contexto.CorrigirOrientacao)
            {
                Log.Application.DebugFormat("Inicio da avaliação de Orientação para processo #{0}.", processoId);
                this.corrigeOrientacaoServico.Executar(paginasDivididasPorSeparadora, this.ObterPastaLocalCache(arquivosParaProcessar));
            }

            if (Contexto.UsarThumbnail)
            {
                Log.Application.DebugFormat("Inicio da geração de 100% thumbnails para processo #{0}.", processoId);
                try
                {
                    var cacheLocalImagens = this.ObterPastaLocalCache(arquivosParaProcessar);
                    this.gerarThumbnailServico.Executar(paginasDivididasPorSeparadora, cacheLocalImagens);
                }
                catch (Exception exception)
                {
                    Log.Application.Error("Erro ao gerar os thumbnails no lote:" + lote.Id, exception);
                }
            } 
            
            if (Contexto.IdentificarTipoPorOcr)
            {
                Log.Application.DebugFormat("Inicio da identificacao de tipos pelo OCR no processo #{0}.", processoId);
                this.identificaTipoDocumentoServico.Executar(paginasDivididasPorSeparadora, this.ObterPastaLocalCache(arquivosParaProcessar));
            }

            Log.Application.DebugFormat("Finalizando criação das entidades no banco de dados no processo #{0}.", processoId);
            foreach (var grupo in paginasDivididasPorSeparadora)
            {
                this.CriarNovoDocumentoInserindoPaginas(grupo, cpf, lote, processo, versaoAtual, ordem);
                ordem++;
            }

            this.MarcarComoExcluidosDocumentosGeradosNaDigitalizacao(documentos);

            return itensComBrancosIdentificados;
        }

        private string ObterPastaLocalCache(List<ItemParaSeparacao> arquivosParaProcessar)
        {
            return Path.GetDirectoryName(arquivosParaProcessar.First().ArquivoBaixado);
        }

        private List<ItemParaSeparacao> ObterArquivosParaProcessar(List<Pagina> paginasJpeg)
        {
            var arquivosParaProcessar = new List<ItemParaSeparacao>();

            foreach (var pagina in paginasJpeg)
            {
                var arquivo = this.baixaArquivoFileTransferServico.BaixarArquivo(pagina.Id, pagina.TipoArquivo);

                var processamentoSeparador = IoC.Current.Resolve<ItemParaSeparacao>();
                processamentoSeparador.Pagina = pagina;
                processamentoSeparador.ArquivoBaixado = arquivo;
                
                arquivosParaProcessar.Add(processamentoSeparador);

                if (this.validacaoImagem.JpegEhValido(arquivo) == false)
                {
                    this.gravaLogDaPaginaServico.Executar(
                        LogPagina.AcaoPaginaCorrompida,
                        pagina.Id,
                        pagina.Documento.Id,
                        "Corrompida na digitalizacao");
                }
            }

            return arquivosParaProcessar;
        }
        
        private void MarcarComoExcluidosDocumentosGeradosNaDigitalizacao(IEnumerable<Documento> documentos)
        {
            foreach (var documento in documentos)
            {
                this.documentoRepositorio.AlterarStatus(documento.Id, DocumentoStatus.Excluido);
            }
        }

        private void CriarNovoDocumentoInserindoPaginas(
            IEnumerable<Pagina> paginasNovas,
            string cpf,
            Lote lote, 
            Processo processo,
            string versao = "0", 
            int ordem = 0)
        {
            var tipoDocAtual = this.DefinirTipoDocumento(paginasNovas);
            var novoDocumento = Documento.Novo(
                tipoDocAtual, 
                cpf, 
                lote, 
                processo, 
                versao);

            novoDocumento.Ordem = ordem;
            this.documentoRepositorio.Salvar(novoDocumento);

            if (tipoDocAtual.Id != TipoDocumento.CodigoNaoIdentificado)
            {
                this.gravaLogDoDocumentoServico.Executar(
                    LogDocumento.AcaoDocumentoReclassificadoPeloOcr, 
                    novoDocumento.Id, 
                    string.Format("Classificado pelo OCR. Tipo [{0}]", tipoDocAtual.Id));
            }

            if (Contexto.ManterPalavrasReconhecidas)
            {
                var palavrasOcr = this.ObterPalavrasOcr(paginasNovas);
                if (string.IsNullOrEmpty(palavrasOcr) == false)
                {
                    var indexPalavras = new Indexacao()
                    {
                        Campo = new Campo { Id = Campo.CampoPalavrasOcr },
                        PrimeiroValor = palavrasOcr,
                        Documento = novoDocumento,
                        DataPrimeiraDigitacao = DateTime.Now,
                        OcrComplementou = true
                    };
                    this.indexacaoRepositorio.Salvar(indexPalavras);
                }                
            }

            foreach (var novaPagina in paginasNovas)
            {
                if (novaPagina.EmBranco || novaPagina.Separadora)
                {
                    novaPagina.Status = PaginaStatus.StatusExcluida;
                }

                novoDocumento.AdicionaPagina(novaPagina);
                this.paginaRepositorio.Salvar(novaPagina);
            }

            Log.Application.DebugFormat("Paginas do documento #{0} salvas com sucesso", novoDocumento.Id);
        }

        private string ObterPalavrasOcr(IEnumerable<Pagina> paginasNovas)
        {
            var primeiraPagina = paginasNovas.FirstOrDefault(x => x.Status != PaginaStatus.StatusExcluida && x.Separadora == false && x.ContrapartidaDeSeparadora == false && x.EmBranco == false);
            if (primeiraPagina == null)
            {
                return string.Empty;
            }            

            if (string.IsNullOrEmpty(primeiraPagina.PalavrasReconhecidasOcr))
            {
                return string.Empty;
            }

            if (primeiraPagina.PalavrasReconhecidasOcr.Length > 3991)
            {
                return primeiraPagina.PalavrasReconhecidasOcr.Substring(0, 3990) + "(...)";
            }

            return primeiraPagina.PalavrasReconhecidasOcr;
        }

        private TipoDocumento DefinirTipoDocumento(IEnumerable<Pagina> paginasNovas)
        {
            var primeiraPagina = paginasNovas.FirstOrDefault(x => x.Status != PaginaStatus.StatusExcluida && x.Separadora == false && x.ContrapartidaDeSeparadora == false && x.EmBranco == false);
            if (primeiraPagina == null)
            {
                return TipoDocumento.CriarNaoIdentificado();
            }

            if (primeiraPagina.TipoDocumentoDefinidoPorOcr != null)
            {
                return primeiraPagina.TipoDocumentoDefinidoPorOcr;
            }

            return TipoDocumento.CriarNaoIdentificado();
        }
    }
}