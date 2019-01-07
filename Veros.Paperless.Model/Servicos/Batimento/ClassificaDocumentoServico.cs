namespace Veros.Paperless.Model.Servicos.Batimento
{
    using Entidades;
    using Framework;
    using Repositorios;
    using System.Collections.Generic;
    using System.Linq;

    public class ClassificaDocumentoServico : IClassificaDocumentoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ITagRepositorio tagRepositorio;
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;

        public ClassificaDocumentoServico(IDocumentoRepositorio documentoRepositorio, 
            ITagRepositorio tagRepositorio, 
            ITipoDocumentoRepositorio tipoDocumentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.tagRepositorio = tagRepositorio;
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
        }

        public void Execute(Documento documento, ImagemReconhecida imagemReconhecida)
        {
            if (!this.PodeFazerClassificacao(documento.TipoDocumento))
            {
                return;
            }

            //// verifica se o OCR reconheceu o tipo do documento. 
            var valorReconhecidoTipo = imagemReconhecida.ValoresReconhecidos.FirstOrDefault(x => x.CampoTemplate.ToLower() == "tipo");
            if (valorReconhecidoTipo == null || string.IsNullOrEmpty(valorReconhecidoTipo.Value))
            {
                return;
            }

            Log.Application.InfoFormat("Tentativa de classificação do documento {0}", documento.Id);

            //// carregar tags e montar listas para cada tipo
            int tipoDocumentoId = this.DefinirTipoPorTags(valorReconhecidoTipo.Value);

            if (tipoDocumentoId != TipoDocumento.CodigoNaoIdentificado)
            {
                documento.TipoDocumento = this.tipoDocumentoRepositorio.ObterPorCodigo(tipoDocumentoId);
                this.documentoRepositorio.Salvar(documento);
                this.gravaLogDocumentoServico.Executar(
                    LogDocumento.AcaoDocumentoOcr,
                    documento.Id,
                    string.Format("Documento reclassificado para [{0}] pelo OCR.", tipoDocumentoId));
                Log.Application.InfoFormat("Documento [{0}] reclassificado para [{1}] pelo OCR.", documento.Id, tipoDocumentoId);
            }
        }

        private int DefinirTipoPorTags(string ocrTipo)
        {
            var tagsRG = this.CarregarTags("info.tipo.ocr.type7");
            if (tagsRG.Any(x => x.ToLower() == ocrTipo.ToLower()))
            {
                return TipoDocumento.CodigoRg;
            }

            var tagsCNH = this.CarregarTags("info.tipo.ocr.type8");
            if (tagsCNH.Any(x => x.ToLower() == ocrTipo.ToLower()))
            {
                return TipoDocumento.CodigoCnh;
            }

            var tagsComprovResidencia = this.CarregarTags("info.tipo.ocr.type33");
            if (tagsComprovResidencia.Any(x => x.ToLower() == ocrTipo.ToLower()))
            {
                return TipoDocumento.CodigoComprovanteDeResidencia;
            }

            return TipoDocumento.CodigoNaoIdentificado;
        }

        private List<string> CarregarTags(string chaveTag)
        {
            var valorTag = this.tagRepositorio.ObterPorNome(chaveTag);
            if (valorTag != null && !string.IsNullOrEmpty(valorTag.Valor))
            {
                return valorTag.Valor.Split(',').ToList();    
            }

            return new List<string>() { string.Empty };
        }

        private bool PodeFazerClassificacao(TipoDocumento tipoDocumento)
        {
            return tipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado;
        }
    }
}   
