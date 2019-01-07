namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using System;
    using System.Linq;
    using Batimento.Experimental;
    using Entidades;
    using Framework;
    using Repositorios;

    public class AtualizaIndexacao : IAtualizaIndexacao
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;

        public AtualizaIndexacao(
            IIndexacaoRepositorio indexacaoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDocumentoServico)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
        }

        public void ApartirDoReconhecimento(ResultadoBatimentoDocumento resultadoBatimentoDocumento)
        {
            var campoBatidos = resultadoBatimentoDocumento
                .Campos
                .Where(campo => campo.Batido);

            foreach (var campo in campoBatidos)
            {
                campo.Indexacao.PrimeiroValor = campo.Indexacao.SegundoValor;
                campo.Indexacao.ValorFinal = campo.Indexacao.SegundoValor;
                campo.Indexacao.ValorUtilizadoParaValorFinal = ValorUtilizadoParaValorFinal.SegundoValor;
                campo.Indexacao.OcrComplementou = true;
                campo.Indexacao.DataPrimeiraDigitacao = DateTime.Now;

                this.indexacaoRepositorio.Salvar(campo.Indexacao);

                var observacao = string.Format(
                    "Documento [{0}] Campo [{1}] complementado por [{2}]", 
                    campo.Indexacao.Documento.Id,
                    campo.Indexacao.Campo.Description,
                    campo.TipoBatimento.ToString());

                Log.Application.DebugFormat(observacao);

                this.gravaLogDocumentoServico.Executar(
                        LogDocumento.AcaoDocumentoOcr,
                        campo.Indexacao.Documento.Id,
                        observacao);
            }
        }

        public void ApartirDoBatimentoComProvaZero(ResultadoBatimentoDocumento resultadoBatimento)
        {
            var campoBatidos = resultadoBatimento
               .Campos
               .Where(campo => campo.Batido);

            foreach (var campo in campoBatidos)
            {
                this.indexacaoRepositorio.SalvarValorFinalBatidoComOcr(campo.Indexacao.Id);
            }
        }
    }
}