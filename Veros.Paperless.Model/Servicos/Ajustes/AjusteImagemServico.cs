namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class AjusteImagemServico : IAjusteImagemServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio;
        private readonly IAplicarAjusteDePaginaPorDocumento aplicarAjusteDePagina;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public AjusteImagemServico(
            IDocumentoRepositorio documentoRepositorio, 
            IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio, 
            IAplicarAjusteDePaginaPorDocumento aplicarAjusteDePagina, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.ajusteDeDocumentoRepositorio = ajusteDeDocumentoRepositorio;
            this.aplicarAjusteDePagina = aplicarAjusteDePagina;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Aplicar(IList<AjusteDeDocumento> ajustesDeDocumento)
        {
            var documentosParaAjustar = ajustesDeDocumento.GroupBy(
                                                x => x.Documento.Id,
                                                (key, g) => new { DocumentoId = key, Ajustes = g.ToList() });

            foreach (var documento in documentosParaAjustar)
            {
                this.gravaLogDoDocumentoServico.Executar(
                        LogDocumento.AcaoDocumentoGenerico,
                        documento.DocumentoId,
                        "Inicio Realizacao Ajuste Documento");

                var temAjusteDePagina = this.aplicarAjusteDePagina.Executar(documento.DocumentoId, documento.Ajustes);

                if (temAjusteDePagina)
                {
                    this.documentoRepositorio
                        .AlterarStatus(documento.DocumentoId, DocumentoStatus.StatusParaReconhecimentoPosAjuste);

                    var ajustesClassificacao = documento.Ajustes.Where(x => x.Acao == AcaoAjusteDeDocumento.Reclassificar);

                    foreach (var ajusteClassificacao in ajustesClassificacao)
                    {
                        this.ajusteDeDocumentoRepositorio.GravarComoProcessado(ajusteClassificacao);
                    }
                }
                else
                {
                    if (documento.Ajustes.Any(x => x.Acao == AcaoAjusteDeDocumento.Reclassificar) == false)
                    {
                        continue;
                    }

                    var documentoAtual = this.documentoRepositorio.ObterPorId(documento.DocumentoId);
                    var documentoPai = this.documentoRepositorio.ObterPorId(documentoAtual.DocumentoPaiId);

                    documentoPai.Status = DocumentoStatus.IdentificacaoConcluida;
                    documentoPai.TipoDocumento = documentoAtual.TipoDocumentoOriginal;
                    documentoPai.IndicioDeFraude = null;
                    this.documentoRepositorio.Salvar(documentoPai);
                    ////this.documentoRepositorio.AlterarStatus(documentoAtual.DocumentoPaiId, DocumentoStatus.IdentificacaoConcluida);
                    ////this.documentoRepositorio.AlterarTipo(documentoAtual.DocumentoPaiId, documentoAtual.TipoDocumentoOriginal.Id);
                        
                    this.documentoRepositorio.AlterarStatus(documento.DocumentoId, DocumentoStatus.Excluido);
                        
                    var ajustesClassificacao = documento.Ajustes.Where(x => x.Acao == AcaoAjusteDeDocumento.Reclassificar);

                    foreach (var ajusteClassificacao in ajustesClassificacao)
                    {
                        this.ajusteDeDocumentoRepositorio.GravarComoProcessado(ajusteClassificacao);
                    }
                }

                this.gravaLogDoDocumentoServico.Executar(
                        LogDocumento.AcaoDocumentoGenerico,
                        documento.DocumentoId,
                        "Termino Realizacao Ajuste Documento");
            }
        }
    }
}