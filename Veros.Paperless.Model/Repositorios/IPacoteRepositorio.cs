namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;
    using ViewModel;

    public interface IPacoteRepositorio : IRepositorio<Pacote>
    {
        Pacote ObterPacoteAbertoNaEstacao(string estacao);

        Pacote ObterPorIdentificacaoCaixa(string barcodeCx);

        IList<int> ObterParaExportacao();

        void MarcarComoExportado(int pacoteId);

        void MarcarComErro(int pacoteId);

        void MarcarParaExportar(Pacote pacote);

        void MarcarParaDevolucao(int pacoteId);

        void MarcarComoDevolvida(int pacoteId);

        void MarcarComoConferida(int pacoteId);

        Pacote ObterPacoteDoProcesso(Processo processo);

        IList<Pacote> ObterPorColeta(Coleta coleta);

        Pacote ObterComColetaPorId(int pacoteId);

        IList<Pacote> ObterPorStatus(string recebido);

        Pacote ObterComDossiesEsperados(int pacoteId);

        IList<Pacote> Pesquisar(PesquisaPacoteViewModel filtros);

        Pacote ObterPorCaixa(string identificacao);

        void MarcarEtiquetaCefGerada(int pacoteId);

        Pacote ObterPorIdentificacaoCaixaEColeta(string identificacao, int coleta);

        void AlterarStatusPorColeta(Coleta coleta, string status);
        
        void ApagarPorColeta(Coleta coleta, string status);

        void AlterarStatus(Pacote pacote, string status);

        void ExcluirPacotePorColeta(int coletaId);

        Pacote ObterCaixa(int pacoteId);
    }
}
