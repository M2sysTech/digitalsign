namespace Veros.Paperless.Model.Servicos.Exportacao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Comparacao;
    using Framework;
    using Veros.Paperless.Model.Entidades;
    using Repositorios;

    public class PacoteParaExportarFabrica
    {
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public PacoteParaExportarFabrica(
            IPacoteRepositorio pacoteRepositorio, 
            IProcessoRepositorio processoRepositorio, 
            IDocumentoRepositorio documentoRepositorio)
        {
            this.pacoteRepositorio = pacoteRepositorio;
            this.processoRepositorio = processoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public PacoteParaExportar Criar(int pacoteId)
        {
            var pacote = this.pacoteRepositorio.ObterComColetaPorId(pacoteId);

            var pacoteParaExportar = new PacoteParaExportar
            {
                Identificacao = pacote.Coleta.Id.ToString(),
                Data = pacote.Coleta.Data,
                Regiao = "PR"
            };

            var processos = this.processoRepositorio.ObterPorPacoteComStatusFinalizado(pacoteId);
            Log.Application.Info("Pacotes encontrados: " + processos.Count);
            pacoteParaExportar = this.ObterComDossiers(pacoteParaExportar, processos);

            return pacoteParaExportar;
        }

        private PacoteParaExportar ObterComDossiers(PacoteParaExportar pacoteParaExportar, IList<Processo> processos)
        {
            foreach (var processo in processos)
            {
                var sequencial = processo.Lote.Identificacao.Substring(processo.Lote.Identificacao.Length - 4, 4);  

                var dossie = new Dossie
                {
                    DataGeracaoXml = DateTime.Now,
                    NumeroContrato = processo.Identificacao,
                    QuantidadePaginas = processo.QtdePaginas,
                    Sequencial = sequencial.ToInt(),
                    TipoDossieDescricao = processo.TipoDeProcesso.Descricao,
                    TipoDossieId = processo.TipoDeProcesso.Id,
                    ProcessoId = processo.Id
                };

                this.AdicionarDocumentosAoDossie(pacoteParaExportar, dossie, sequencial, processo);
            }

            return pacoteParaExportar;
        }

        private void AdicionarDocumentosAoDossie(PacoteParaExportar pacoteParaExportar, Dossie dossie, string sequencial, Processo processo)
        {
            var documentos = this.documentoRepositorio
                .ObterTodosPorLoteComTipoDocumento(processo.Lote.Id);

            ////a) NUMERO_CONTRATO+TIPO_CONTRATO+CODIGO_TIPO_DOCUMENTO+CODIGO_SEQUENCIA.PDF
            var somentePdfs = documentos.Where(
                x => x.Status != DocumentoStatus.Excluido &&
                x.Virtual);

            foreach (var documento in somentePdfs)
            {
                var nomePdf = string.Format(
                    "{0}_{1}_{2}_{3}", 
                    processo.Identificacao.RemoverDiacritico().Replace(@"/", string.Empty), 
                    processo.TipoDeProcesso.Descricao.RemoverDiacritico().RemoverEspacosEntreAsPalavras(), 
                    documento.TipoDocumento.Id, 
                    documento.Id);

                var itemDocumental = new ItemDocumental
                {
                    NumeroDossie = processo.Identificacao,
                    TipoDocumento = documento.TipoDocumento.Description,
                    NomeParaArquivo = nomePdf,
                    DocumentoId = documento.Id
                };

                dossie.AdicionarItemDocumental(itemDocumental);
            }

            pacoteParaExportar.AdicionarDossier(dossie);
        }
    }
}