namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;
    
    public class ExecutaAjusteDeReclassificacaoServico : IExecutaAjusteDeReclassificacaoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;

        public ExecutaAjusteDeReclassificacaoServico(IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            ITipoDocumentoRepositorio tipoDocumentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;
        }

        public void Executar(IEnumerable<AjusteDeDocumento> ajustes)
        {
            var documentosReclassificados = new List<Documento>();

            foreach (var ajuste in ajustes.Where(x => x.Acao == AcaoAjusteDeDocumento.Reclassificar).OrderByDescending(x => x.Id))
            {
                if (documentosReclassificados.Any(x => x == ajuste.Documento))
                {
                    continue;
                }

                documentosReclassificados.Add(ajuste.Documento);

                this.documentoRepositorio.AlterarTipoOriginal(ajuste.Documento, ajuste.TipoDocumentoNovo);
                this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoDocumentoReclassificadoNoAjuste, ajuste.Documento.Id, "Documento reclassificado no ajuste");

                this.ReclassificarFilhos(ajuste.Documento.Id, ajuste.TipoDocumentoNovo.Id);
            }
        }

        public void ReclassificarFilhos(int documentoId, int novoTipoDocumentoId)
        {
            var documento = this.documentoRepositorio.ObterPorId(documentoId);

            var tipoDocumento = this.tipoDocumentoRepositorio.ObterPorId(novoTipoDocumentoId);

            var documentoPai = this.documentoRepositorio.ObterPorId(documento.DocumentoPaiId);
            this.Reclassificar(documentoPai, tipoDocumento);

            if (documentoPai != null)
            {
                var documentosFilhos = this.documentoRepositorio.ObterFilhos(documento.Lote.Id, documentoPai.Id);
                foreach (var documentoFilho in documentosFilhos)
                {
                    this.Reclassificar(documentoFilho, tipoDocumento);
                }    
            }
        }

        private void Reclassificar(Documento documento, TipoDocumento tipoDocumento)
        {
            if (documento == null || documento.TipoDocumento.Id == TipoDocumento.CodigoEmAjuste)
            {
                return;
            }

            documento.Reclassificado = true;
            documento.TipoDocumento = tipoDocumento;

            this.documentoRepositorio.Salvar(documento);

            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoDocumentoReclassificado,
                    documento.Id,
                    "Documento reclassificado na separação - Tipo: " + tipoDocumento.Description);
        }
    }
}
