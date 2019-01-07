namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;
    using Identificacao;
    using Repositorios;

    /// <summary>
    /// TODO: Fortalecer testes
    /// </summary>
    public class BatimentoEComplementacaoNaoIdentificado :
        IBatimentoEComplementacaoDocumentoServico
    {
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;
        private readonly IAlteraIndexacaoServico alteraIndexacaoServico;
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public BatimentoEComplementacaoNaoIdentificado(
            IGravaLogDoDocumentoServico gravaLogDocumentoServico,
            IAlteraIndexacaoServico alteraIndexacaoServico,
            ITipoDocumentoRepositorio tipoDocumentoRepositorio,
            IDocumentoRepositorio documentoRepositorio)
        {
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
            this.alteraIndexacaoServico = alteraIndexacaoServico;
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Execute(Documento documento, ImagemReconhecida imagemReconhecida, List<int> camposBatidosId = null)
        {
            if (imagemReconhecida == null)
            {
                return;
            }

            var valoresReconhecidos = imagemReconhecida.ValoresReconhecidos;

            if (valoresReconhecidos == null)
            {
                return;
            }

            var codigoDoTipoDoDocumento = this.ObterCodigoTipoDocumentoApartirValoresReconhecidos(valoresReconhecidos);

            if (this.CodigoEhValido(codigoDoTipoDoDocumento) == false)
            {
                this.gravaLogDocumentoServico.Executar(
                    LogDocumento.AcaoDocumentoOcr,
                    documento.Id,
                    "Não foi possivel reclassificar documento 'Não Identificado' no OCR.");

                Log.Application.InfoFormat("Tentiva de reclassificação do Documento #{0} falhou.", documento.Id);

                return;
            }

            TipoDocumento novoTipoDocumento;

            var cpfDocumento = documento
                .Indexacao
                .Where(x => x.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoCpf);

            var documentosDoParticipante = this.documentoRepositorio
                .ObterDocumentosDoProcessoComCpf(
                    documento.Processo.Id, 
                    cpfDocumento.ElementAt(0).ObterValor());

            ////if (codigoDoTipoDoDocumento == TipoDocumento.CodigoCn)
            ////{
            ////    if (this.JaPossuiDocumentoIdentificacao(documentosDoParticipante))
            ////    {
            ////        novoTipoDocumento = this.tipoDocumentoRepositorio.ObterPorCodigo(TipoDocumento.CodigoDocumentoGeral);

            ////        documento.TipoDocumento = novoTipoDocumento;
            ////        this.alteraIndexacaoServico.Alterar(documento, novoTipoDocumento);

            ////        this.gravaLogDocumentoServico.Executar(
            ////            LogDocumento.AcaoDocumentoOcr,
            ////            documento.Id,
            ////            "Documento classificado como [Documento Geral]. Documento foi reconhecido como Certidão de nascimento e já existe um documento de identificação na conta");

            ////        return;
            ////    }
            ////}

            if (this.JaPossuiDocumentoIdentificacao(documentosDoParticipante) == false)
            {
                this.gravaLogDocumentoServico.Executar(
                    LogDocumento.AcaoDocumentoOcr,
                    documento.Id,
                    "Documento não foi reclassificado. Não existe documento de identificação para este participamente");

                return;
            }

            novoTipoDocumento = this.tipoDocumentoRepositorio.ObterPorCodigo(codigoDoTipoDoDocumento);

            documento.TipoDocumento = novoTipoDocumento;

            this.alteraIndexacaoServico.Alterar(documento, novoTipoDocumento);

            this.gravaLogDocumentoServico.Executar(
                LogDocumento.AcaoDocumentoOcr,
                documento.Id,
                string.Format("Documento reclassificado para [{0}] pelo OCR.", novoTipoDocumento.Description));

            Log.Application.InfoFormat("Documento #{0} reclassificado pelo OCR para tipo #{1}", documento.Id, codigoDoTipoDoDocumento);
        }

        private bool CodigoEhValido(int codigoDoTipoDoDocumento)
        {
            return codigoDoTipoDoDocumento != 0;
        }

        private int ObterCodigoTipoDocumentoApartirValoresReconhecidos(IList<ValorReconhecido> valoresReconhecidos)
        {
            int codigoEscolhido = 0;

            foreach (var valorReconhecido in valoresReconhecidos)
            {
                if (valorReconhecido.CampoTemplate.ToLower() == "tipo")
                {
                    if (string.IsNullOrEmpty(valorReconhecido.Value))
                    {
                        return codigoEscolhido;
                    }

                    switch (valorReconhecido.Value.ToLower())
                    {
                        case "aes": 
                        case "vivo": 
                        case "tim": 
                        case "net":
                            codigoEscolhido = TipoDocumento.CodigoComprovanteDeResidencia;
                            break;
                        case "rg":
                            codigoEscolhido = TipoDocumento.CodigoRg;
                            break;
                        case "cnh":
                            codigoEscolhido = TipoDocumento.CodigoCnh;
                            break;
                        default:
                            codigoEscolhido = TipoDocumento.CodigoNaoIdentificado;
                            break;
                    }
                }
            }

            return codigoEscolhido;
        }

        private bool JaPossuiDocumentoIdentificacao(IList<Documento> documentosDoParticipante)
        {
            return documentosDoParticipante.Count(
                x => x.TipoDocumento.Id == TipoDocumento.CodigoCnh ||
                x.TipoDocumento.Id == TipoDocumento.CodigoRg ||
                x.TipoDocumento.Id == TipoDocumento.CodigoCie ||
                x.TipoDocumento.Id == TipoDocumento.CodigoPassaporte) > 0;
        }
    }
}
