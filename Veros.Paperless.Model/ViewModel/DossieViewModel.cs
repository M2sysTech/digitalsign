namespace Veros.Paperless.Model.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;

    public class DossieViewModel
    {
        public int LoteId { get; set; }

        public int ProcessoId { get; set; }

        public string Tipo { get; set; }
        
        public string Identificacao { get; set; }
        
        public DateTime DataRegistro { get; set; }

        public DateTime? DataMovimento { get; set; }
        
        public string TempoDeProcessamento { get; set; }

        public string Caixa { get; set; }

        public string Folder { get; set; }

        public string Status { get; set; }

        public bool PodeEditar { get; set; }

        public IList<DocumentoViewModel> Documentos { get; set; }

        public int TipoId { get; set; }

        public int ColetaId { get; set; }

        public int LotecefId { get; set; }

        public string LotecefData { get; set; }

        public string LotecefFaturado { get; set; }

        public string UltimaOcorrenciaObs { get; set; }

        public static DossieViewModel Criar(Processo processo, bool carregarDocumentos = false)
        {
            var dossie = new DossieViewModel
            {
                LoteId = processo.Lote.Id,
                ProcessoId = processo.Id,
                Identificacao = processo.Identificacao,
                DataRegistro = processo.Lote.DataCriacao,
                DataMovimento = processo.Lote.PacoteProcessado != null ? processo.Lote.PacoteProcessado.ArquivoRecebidoEm : null,
                Status = processo.Status.DisplayName,
                TempoDeProcessamento = processo.Lote.TempoProcessamento(),
                Caixa = processo.Lote.Pacote.Identificacao,
                Folder = processo.Lote.DossieEsperado.CodigoDeBarras,
                Tipo = processo.TipoDeProcesso.Descricao,
                TipoId = processo.TipoDeProcesso.Id,
                Documentos = new List<DocumentoViewModel>(),
                PodeEditar = processo.Status == ProcessoStatus.Finalizado || processo.Status == ProcessoStatus.AguardandoExportacao || processo.Lote.Status == LoteStatus.Faturamento || processo.Lote.Status == LoteStatus.Finalizado,
                ColetaId = processo.Lote.Pacote.ColetaId,
                LotecefId = processo.Lote.LoteCef == null ? 0 : processo.Lote.LoteCef.Id,
                LotecefData = processo.Lote.LoteCef == null ? string.Empty : (processo.Lote.LoteCef.DataFim == null ? string.Empty : processo.Lote.LoteCef.DataFim.Value.ToString("dd/MM/yyyy")),
                LotecefFaturado = processo.Lote.LoteCef == null ? string.Empty : (processo.Lote.LoteCef.DataAssinaturaCertificado == null ? string.Empty : processo.Lote.LoteCef.DataAssinaturaCertificado.Value.ToString("dd/MM/yyyy")),
                UltimaOcorrenciaObs = processo.Lote.DossieEsperado.UltimaOcorrencia == null ? string.Empty : processo.Lote.DossieEsperado.UltimaOcorrencia.Observacao
            };

            if (processo.Status == ProcessoStatus.AguardandoTransmissao || processo.Status == ProcessoStatus.AguardandoAjuste)
            {
                dossie.Status = processo.Lote.Status.DisplayName;
            }

            if (carregarDocumentos)
            {
                foreach (var documento in processo.Documentos)
                {
                    dossie.Documentos.Add(DocumentoViewModel.Criar(documento));
                }

                var documentosPdfMarcados = dossie.Documentos.Where(x =>
                    x.TipoId == TipoDocumento.CodigoDossiePdf
                    && x.MarcaDeNovaVersao == Documento.SimboloEngrenagem);

                if (documentosPdfMarcados.Any())
                {
                    var pdfMarcado = documentosPdfMarcados.OrderBy(x => x.DocumentoId).Last();

                    var novoPdf = new DocumentoViewModel
                    {
                        DocumentoId = -1,
                        TipoArquivo = pdfMarcado.TipoArquivo,
                        TipoDescricao = pdfMarcado.TipoDescricao,
                        TipoId = pdfMarcado.TipoId,
                        Versao = (pdfMarcado.Versao.ToInt() + 1).ToString()
                    };

                    dossie.Documentos.Add(novoPdf);
                }
            }

            return dossie;
        }
    }
}
