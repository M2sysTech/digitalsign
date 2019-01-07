namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;
    using Framework.IO;
    using Image.Reconhecimento;

    public class ReconhecerOrdenarPaginasPacServico
    {
        private readonly IBaixaArquivoFileTransferServico baixarArquivoFileTransfer;
        private readonly IRecortaCabecalhoRodapeDocumentoPacServico recortaCabecalhoRodapeDocumentoPac;
        private readonly IJuntaImagensDeCabecalhoRodapePacServico juntaImagensDeCabecalhoRodapePac;
        private readonly IOrdenarPaginasPacServico ordenarPaginasPacServico;
        private readonly IFileSystem fileSystem;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;
        private IReconheceImagem reconheceImagem;

        public ReconhecerOrdenarPaginasPacServico(
            IBaixaArquivoFileTransferServico baixarArquivoFileTransfer, 
            IRecortaCabecalhoRodapeDocumentoPacServico recortaCabecalhoRodapeDocumentoPac, 
            IJuntaImagensDeCabecalhoRodapePacServico juntaImagensDeCabecalhoRodapePac,
            IOrdenarPaginasPacServico ordenarPaginasPacServico, 
            IFileSystem fileSystem, 
            IGravaLogDoDocumentoServico gravaLogDocumentoServico)
        {
            this.baixarArquivoFileTransfer = baixarArquivoFileTransfer;
            this.recortaCabecalhoRodapeDocumentoPac = recortaCabecalhoRodapeDocumentoPac;
            this.juntaImagensDeCabecalhoRodapePac = juntaImagensDeCabecalhoRodapePac;
            this.ordenarPaginasPacServico = ordenarPaginasPacServico;
            this.fileSystem = fileSystem;
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
        }

        public void Executar(Documento documentoPac)
        {
            if (documentoPac.TipoDocumento.TipoDocumentoEhMestre == false)
            {
                Log.Application.DebugFormat("Documento #{0} não é uma pac", documentoPac.Id);
                return;
            }

            this.gravaLogDocumentoServico.Executar(
                LogDocumento.AcaoDocumentoOcr,
                documentoPac.Id,
                string.Format("Documento processado no ocr pela máquina {0}", Environment.MachineName));

            Log.Application.DebugFormat("Iniciando ordenação das paginas da pac #{0} ", documentoPac.Id);

            var recortesPac = new List<string>();
            var paginasPac = documentoPac.Paginas.ToList();

            if (paginasPac.Count == 0)
            {
                Log.Application.Error("Documento mestre sem páginas");
                return;
            }

            foreach (var pagina in paginasPac)
            {
                var recortesPagina = this.ObterRecortesDaPagina(documentoPac, pagina);

                if (recortesPagina == null)
                {
                    return;
                }

                recortesPac.AddRange(recortesPagina);
            }

            var imagemJuntada = this.juntaImagensDeCabecalhoRodapePac
                .Executar(recortesPac.ToArray());

            ResultadoReconhecimento resultadoReconhecimento;

            try
            {
                this.reconheceImagem = IoC.Current.Resolve<IReconheceImagem>();

                resultadoReconhecimento = this.reconheceImagem
                        .Reconhecer(imagemJuntada, Image.TipoDocumentoReconhecivel.CabecalhoRodapePac);
            }
            catch (System.Exception exception)
            {
                Log.Application.Error("Não foi possível ordenar paginas da pac.", exception);
                return;
            }

            ////this.ordenarPaginasPacServico.Executar(paginasPac, resultadoReconhecimento);
        }

        private IList<string> ObterRecortesDaPagina(Documento documentoPac, Pagina pagina)
        {
            var imagemPagina = this.baixarArquivoFileTransfer.BaixarArquivo(pagina.Id, pagina.TipoArquivo);

            if (this.fileSystem.Exists(imagemPagina) == false)
            {
                Log.Application.DebugFormat("Imagem da pagina #{0} não foi encontrada", pagina.Id);
                return null;
            }

            var recortesPagina = this.recortaCabecalhoRodapeDocumentoPac
                .Executar(documentoPac.Id, imagemPagina);

            return recortesPagina;
        }
    }
}