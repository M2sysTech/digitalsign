namespace Veros.Paperless.Model.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Consultas;
    using Entidades;
    using Framework.Modelo;

    public interface IPacoteProcessadoRepositorio : IRepositorio<PacoteProcessado>
    {
        PacoteProcessado ObterPorPacote(string pacote);

        PacoteProcessado ObterUltimoArquivoRecebido();

        IList<PacoteProcessado> ObterPendentesDeImportacao();

        IList<PacoteProcessado> ObterPendenciasDeFaturamentoPorPacote();

        IList<PacoteProcessado> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal);

        IList<PacoteProcessado> ObterTodosOsPacotesPendentesDeFaturamento();
        
        IList<PacoteProcessado> ObterPendenciasDeFaturamentoPorPacoteParaDataReferencia(DateTime dataParaFaturamento);

        DateTime? ObterDataParaExpurgo(int ultimosDias);
        
        void AtualizarStatus(int id, StatusPacote statusPacote);
        
        void AlterarPacoteParaFaturado(int id);

        void GravarFimRecepcao(int pacoteProcessadoId);

        IList<PacoteProcessado> ObterPacotesParaExpurgo(int intervaloDeDias);
        
        PacoteProcessado ObterPacoteDoDia();

        Pacote ObterPacotePorBarcodeCaixa(string barcodeCaixa);
        
        IList<PacoteProcessado> ObterPendentes();

        IList<PacoteProcessado> ObterAprovadosCef();
        
        void GravarFimControleDeQualidade(int pacoteProcessadoId, StatusPacote novoStatus);

        void AlterarStatus(int pacoteProcessadoId, StatusPacote novoStatus);

        void AlterarFlagAtivado(int pacoteProcessadoId, string situacao);

        void AlterarFlagExibeNaQualidadeCef(int pacoteProcessadoId, string novaSituacao);
    }
}
