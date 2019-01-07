namespace Veros.Paperless.Model.Servicos.Documentoscopia
{
    using System;
    using System.Linq;
    using Entidades;
    using Framework;
    using Model.Documentoscopia;

    public class ConsultaDocumentoscopiaServico
    {
        private readonly IPortalDocumentoscopia portalDocumentoscopia;
        private readonly GravaResultadoConsultaDocumentoscopiaServico gravaResultadoConsultaDocumentoscopiaServico;
        private readonly IGravaLogDoProcessoServico gravaLogDoProcessoServico;

        public ConsultaDocumentoscopiaServico(
            IPortalDocumentoscopia portalDocumentoscopia,
            GravaResultadoConsultaDocumentoscopiaServico gravaResultadoConsultaDocumentoscopiaServico,
            IGravaLogDoProcessoServico gravaLogDoProcessoServico)
        {
            this.portalDocumentoscopia = portalDocumentoscopia;
            this.gravaResultadoConsultaDocumentoscopiaServico = gravaResultadoConsultaDocumentoscopiaServico;
            this.gravaLogDoProcessoServico = gravaLogDoProcessoServico;
        }

        public void Execute(Processo processo)
        {
            try
            {
                var documentoIdentificacao = processo.Documentos.First(x => x.TipoDocumento.Id == TipoDocumento.CodigoRg || x.TipoDocumento.Id == TipoDocumento.CodigoCnh);

                if (documentoIdentificacao == null)
                {
                    return;
                }

                var tipoDeDocumento = documentoIdentificacao.TipoDocumento.Id == TipoDocumento.CodigoRg ? "RG" : "CNH";

                var uf = this.ObterValorParaConsulta(documentoIdentificacao, Campo.ReferenciaDeArquivoUfDi);
                var dataEmissao = this.ObterValorParaConsulta(documentoIdentificacao, Campo.ReferenciaDeArquivoDataDeEmissaoDoDocumento);

                if (string.IsNullOrEmpty(uf) || uf.Length < 2 || string.IsNullOrEmpty(dataEmissao) || dataEmissao.Length < 2)
                {
                    var observacao = string.Format("Não consultou documentoscopia. UF:{0} e Emissao:{1}",
                        uf,
                        dataEmissao);

                    this.gravaLogDoProcessoServico.Executar(LogProcesso.AcaoNaoConsultaDocumentoscopia, processo, observacao);
                    return;
                }

                var resultado = this.portalDocumentoscopia.ObterValidacao(tipoDeDocumento,
                    uf,
                    dataEmissao,
                    this.ObterValorParaConsulta(documentoIdentificacao, Campo.ReferenciaDeArquivoGraficaDeImpressao),
                    this.ObterValorParaConsulta(documentoIdentificacao, Campo.ReferenciaDeArquivoCpf),
                    this.ObterValorParaConsulta(documentoIdentificacao, Campo.ReferenciaDeArquivoNumeroDoRegistro),
                    this.ObterValorParaConsulta(documentoIdentificacao, Campo.ReferenciaDeArquivoNaturalidade),
                    this.ObterValorParaConsulta(documentoIdentificacao, Campo.ReferenciaDeArquivoNumeroDoDi),
                    this.ObterValorParaConsulta(documentoIdentificacao, Campo.ReferenciaDeArquivoAssinaturaDi));

                this.gravaResultadoConsultaDocumentoscopiaServico.Execute(processo, resultado);
            }
            catch (Exception exception)
            {
                Log.Application.Error(
                                "Nao foi possível consultar documentoscopia do processo #" + processo.Id,
                                exception);

                try
                {
                    var observacao = string.Format("Erro ao consultar documentoscopia. Erro: {0}", exception.Message);

                    this.gravaLogDoProcessoServico.Executar(LogProcesso.AcaoNaoConsultaDocumentoscopia, processo, observacao);
                }
                catch (Exception e)
                {
                }
            }
        }

        private string ObterValorParaConsulta(Documento documentoIdentificacao, string referenciaDeArquivoUfDi)
        {
            var valor = documentoIdentificacao.ObterValorFinal(referenciaDeArquivoUfDi);

            return string.IsNullOrEmpty(valor) || valor == "#" || valor == "?" ? string.Empty : valor;
        }
    }
}
