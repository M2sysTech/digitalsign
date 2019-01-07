namespace Veros.Paperless.Model.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Consultas;
    using Entidades;
    using Framework.Modelo;

    public interface IEstatisticaAprovacaoRepositorio : IRepositorio<EstatisticaAprovacao>
    {
        EstatisticaAprovacao ObterDeHojePorUsuario(int usuarioId);

        SomaDeEstatisticaDeAprovacaoConsulta ObterSomaPorUsuarioAndData(int usuarioId, DateTime data);

        SomaDeEstatisticaDeAprovacaoConsulta ObterSomaPorData(DateTime data);

        IList<SomaDeEstatisticaDeAprovacaoConsulta> ObterSomaPorPeriodo(
            DateTime dataInicial,
            DateTime dataFinal);

        EstatisticaAprovacao ObterProducaoPorUsuarioAndData(int usuarioId, DateTime data);

        IList<SomaDeEstatisticaDeAprovacaoConsulta> ObterSomaPorUsuarioAndPeriodo(
            int usuarioId,
            DateTime dataInicial,
            DateTime dataFinal);
        
        void IncrementarDevolvidasParaHoje(int usuarioId);

        void IncrementarDevolvidasComFraudeParaHoje(int usuarioId);

        void IncrementarLiberadasParaHoje(int usuarioId);

        void IncrementarLiberadasComFraudeParaHoje(int usuarioId);

        void IncrementarAbandonadasParaHoje(int usuarioId);

        void IncrementarFraudesParaHoje(int usuarioId);

        IList<EstatisticaAprovacao> ObterPorPeriodo(DateTime dataInicio, DateTime dataFim);
        
        IList<EstatisticaAprovacao> ObterPorPeriodoEUsuario(DateTime dataInicio, DateTime dataFim, int usuarioId);
    }
}
