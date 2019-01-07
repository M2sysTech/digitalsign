namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Consultas;
    using Entidades;
    using Framework;
    using Framework.Modelo;
    using Repositorios;

    public class SolicitaNovaAmostraServico : ISolicitaNovaAmostraServico
    {
        private readonly ITotalDeLotesPorSituacaoAmostragemConsulta totalDeLotesPorSituacaoAmostragemConsulta;
        private readonly ITagRepositorio tagRepositorio;
        private readonly IObtemLotesParaAmostragemQualidadeCefConsulta obtemLotesParaAmostragemQualidadeCefConsulta;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;

        public SolicitaNovaAmostraServico(
            ITotalDeLotesPorSituacaoAmostragemConsulta totalDeLotesPorSituacaoAmostragemConsulta, 
            ITagRepositorio tagRepositorio,
            IObtemLotesParaAmostragemQualidadeCefConsulta obtemLotesParaAmostragemQualidadeCefConsulta, 
            IGravaLogDoLoteServico gravaLogDoLoteServico, 
            ILoteRepositorio loteRepositorio, 
            IProcessoRepositorio processoRepositorio)
        {
            this.totalDeLotesPorSituacaoAmostragemConsulta = totalDeLotesPorSituacaoAmostragemConsulta;
            this.tagRepositorio = tagRepositorio;
            this.obtemLotesParaAmostragemQualidadeCefConsulta = obtemLotesParaAmostragemQualidadeCefConsulta;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.loteRepositorio = loteRepositorio;
            this.processoRepositorio = processoRepositorio;
        }

        public void Executar(int loteCefId)
        {
            var quantidadeDeLotesPorSituacao = this.totalDeLotesPorSituacaoAmostragemConsulta.Obter(loteCefId);

            if (quantidadeDeLotesPorSituacao.Any(x => x.SituacaoAmostragem == 0) == false)
            {
                throw new RegraDeNegocioException("Lote não possui dossiês disponíveis!");
            }

            if (quantidadeDeLotesPorSituacao.Any(x => x.SituacaoAmostragem > 2))
            {
                throw new RegraDeNegocioException("Limite de 3 solicitações atingido!");
            }

            var quantidadeDeLotes = this.ObterQuantidadeDeLotes(quantidadeDeLotesPorSituacao);
            var lotesId = this.obtemLotesParaAmostragemQualidadeCefConsulta.Executar(loteCefId, quantidadeDeLotes);

            if (lotesId == null || lotesId.Any() == false)
            {
                throw new RegraDeNegocioException("Nenhum dossiê com status disponível para amostra!");
            }

            var novaSituacaoAmostragem = quantidadeDeLotesPorSituacao.Max(x => x.SituacaoAmostragem) + 1;

            foreach (var loteView in lotesId)
            {
                this.gravaLogDoLoteServico.Executar(LogLote.AcaoAdicionaNaQualidadeCef, 
                    loteView.Codigo, 
                    "Lote adicionado na qualidade CEF manualmente.");

                this.loteRepositorio.EnviarParaQualidadeCef(loteView.Codigo, novaSituacaoAmostragem);
                this.processoRepositorio.EnviarParaQualidadeCef(loteView.Codigo);
            }
        }

        private int ObterQuantidadeDeLotes(IEnumerable<TotalDeLotesPorPacotePorSituacaoAmostragem> totaisDeLote)
        {
            var percentual = this.tagRepositorio.ObterValorPorNome(Tag.PercentualQualidadeCef, "3").ToInt();
            var totalDeLotes = totaisDeLote.Sum(x => x.Total);

            var quantidade = (int)Math.Ceiling(totalDeLotes * (percentual / 100F));

            return quantidade < 1 ? 1 : quantidade;
        }
    }
}
