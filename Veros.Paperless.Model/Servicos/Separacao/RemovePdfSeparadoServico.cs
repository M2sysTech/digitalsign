namespace Veros.Paperless.Model.Servicos.Separacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class RemovePdfSeparadoServico : IRemovePdfSeparadoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public RemovePdfSeparadoServico(IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var documentosDoProcesso = this.documentoRepositorio.ObterPorProcesso(loteParaSeparacao.ProcessoId);
            var documentosAlterados = loteParaSeparacao.ObterDocumentosAlterados();

            foreach (var documentoId in documentosAlterados)
            {
                var documentosFilhos = this.ObterFilhos(documentosDoProcesso, documentoId);

                if (documentosFilhos == null || documentosFilhos.Any() == false)
                {
                    continue;
                }

                this.ExcluirDocumentos(documentosDoProcesso, documentosFilhos);
            }
        }

        private void ExcluirDocumentos(IEnumerable<Documento> documentosDoProcesso, IEnumerable<Documento> documentos)
        {
            foreach (var documento in documentos)
            {
                if (documento.Virtual)
                {
                    documento.Status = DocumentoStatus.Excluido;
                    this.documentoRepositorio.AlterarStatus(documento.Id, DocumentoStatus.Excluido);
                    this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoExcluidoNaSeparacao, documento.Id, "Documento foi excluído na sepação pois um novo PDF será gerado.");    
                }

                var filhos = this.ObterFilhos(documentosDoProcesso, documento.Id);

                if (filhos != null && filhos.Any())
                {
                    this.ExcluirDocumentos(documentosDoProcesso, filhos);
                }
            }
        }

        private IEnumerable<Documento> ObterFilhos(IEnumerable<Documento> documentosDoProcesso, int documentoId)
        {
            return documentosDoProcesso.Where(x => x.DocumentoPaiId == documentoId).ToList();
        }
    }
}
