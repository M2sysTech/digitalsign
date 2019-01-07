namespace Veros.Paperless.Model.Servicos.Identificacao
{
    using Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class AlteraTipoDeDocumentoServico : IAlteraTipoDeDocumentoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private readonly IAlteraIndexacaoServico alteraIndexacaoServico;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IRankingReclassificacaoService rankingReclassificacaoService;
        private readonly ICriaPaginaNaPacServico criaPaginaNaPacServico;

        public AlteraTipoDeDocumentoServico(
                    IDocumentoRepositorio documentoRepositorio, 
                    ITipoDocumentoRepositorio tipoDocumentoRepositorio, 
                    IAlteraIndexacaoServico alteraIndexacaoServico, 
                    IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
                    ILoteRepositorio loteRepositorio,
                    IRankingReclassificacaoService rankingReclassificacaoService, 
                    ICriaPaginaNaPacServico criaPaginaNaPacServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;
            this.alteraIndexacaoServico = alteraIndexacaoServico;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.loteRepositorio = loteRepositorio;
            this.rankingReclassificacaoService = rankingReclassificacaoService;
            this.criaPaginaNaPacServico = criaPaginaNaPacServico;
        }

        public void Alterar(int documentoId, int tipoDocumentoId)
        {
            var documento = this.documentoRepositorio.ObterPorId(documentoId);

            if (documento == null || documento.TipoDocumento.Id != TipoDocumento.CodigoNaoIdentificado)
            {
                return;
            }

            if (tipoDocumentoId == TipoDocumento.CodigoPaginaDaPac)
            {
                this.criaPaginaNaPacServico.Criar(documento);
                tipoDocumentoId = TipoDocumento.CodigoDocumentoGeral;
            }

            var tipoDocumentoNovo = this.tipoDocumentoRepositorio.ObterPorId(tipoDocumentoId);

            this.rankingReclassificacaoService.IncrementarRankDeReclassificacao(tipoDocumentoNovo, documento);

            this.alteraIndexacaoServico.Alterar(documento, tipoDocumentoNovo);

            this.AlterarTipo(documento, tipoDocumentoNovo);

            this.GravarLog(documento);

            ////TODO: O WF está realizando essa alteração
            ////this.AlterarStatusDoLote(documento.Lote.Id);
        }

        private void AlterarTipo(Documento documento, TipoDocumento tipoDocumentoNovo)
        {
            var statusDeConsulta = tipoDocumentoNovo.Id == TipoDocumento.CodigoComprovanteDeResidencia ? 
                DocumentoStatus.StatusDeConsultaPendente :
                DocumentoStatus.StatusDeConsultaRealizado;

            documento.TipoDocumento = tipoDocumentoNovo;

            this.documentoRepositorio.Reclassificar(documento.Id, tipoDocumentoNovo.Id, statusDeConsulta);
        }

        private void GravarLog(Documento documento)
        {
            this.gravaLogDoDocumentoServico.Executar(
                LogDocumento.AcaoDocumentoIdentificacao, 
                documento.Id,
                string.Format("Tipo de documento alterado para {0}", documento.TipoDocumento.Id));
        }

        private void AlterarStatusDoLote(int loteId)
        {
            ////TODO: Essa funcionalidade não ficará aqui. (será missão do WF) Por isso não foi criado um serviço separado.
            if (this.documentoRepositorio.ObterDocumentosDoLotePorTipo(loteId, TipoDocumento.CodigoNaoIdentificado).Count == 0)
            {
                this.loteRepositorio.AlterarStatus(loteId, LoteStatus.Identificado);
            }
        }
    }
}