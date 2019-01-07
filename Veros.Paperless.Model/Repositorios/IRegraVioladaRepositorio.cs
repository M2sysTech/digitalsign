namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Framework.Modelo;
    using Veros.Paperless.Model.Entidades;

    public interface IRegraVioladaRepositorio : IRepositorio<RegraViolada>
    {
        IList<RegraViolada> ObterRegrasVioladasParaAprovacao(int processoId);

        void AlterarStatus(int id, RegraVioladaStatus status);

        IList<RegraViolada> ObterRegrasVioladasParaValidacaoPorProcesso(int processoId);

        IList<RegraViolada> ObterRegrasVioladasParaValidacaoPorDocumento(int documentoId);

        IList<RegraViolada> ObterRegrasVioladasParaExportacaoPorDocumento(int documentoId);

        bool ExistePorProcessoERegraEStatus(int processoId, int regraId, string statusDaRegraViolada);

        int ObterTotalPorProcessoEStatusRegra(int regraId, int processoId, string statusRegra);

        IList<RegraViolada> ObterTodasPorProcesso(int processoId);

        bool ExisteRegraDeFraude(int processoId);

        bool ExisteRegraDeDivergenciaNoProcesso(int processoId);

        IList<RegraViolada> ObterRegrasDeFraude(int processoId);

        IList<RegraViolada> ObterRegrasVioladasParaExportacaoPorProcesso(int processoId);

        int ObterQuantidadeRegrasVioladasParaProvaZeroPorProcesso(int processoId);

        bool ExistePorProcessoERegraEStatus(int processoId, int regraId, RegraVioladaStatus status);

        int ObterTotalPorProcessoEStatusRegra(int regraId, int processoId, RegraVioladaStatus status);

        bool ExisteRegraVioladaPorProcesso(int processoId);

        bool ExisteRegraVioladaSemTratamento(int processoId, string fase);

        Regra ObterRegraPorId(int regraVioladaId);

        RegraViolada ObterRegraViloladaPorId(int regraVioladaId);

        void ExcluirRegraDoProcesso(int processoId, int regraId);
        
        bool ExisteRegraDeErroVioladaSemTratamento(int processoId, string faseDeRegra);
        
        IList<RegraViolada> ObterVioladasParaExportacao(int processoId);
        
        bool ExisteRegraPendenteDeRevisao(int processoId);

        bool ExisteRegraFinalizacaoNeurotech(int processoId);

        bool ExisteRegraVioladaPorRegra(int processoId, int regraId);

        IList<RegraViolada> ObterRegrasVioladasParaDetalhe(int processoId);

        IList<RegraViolada> ObterPorProcesso(int processoId, int regraId);

        IList<RegraViolada> ObterRegrasDeQualidade(int processoId);
        
        void AlterarStatus(Processo processo, int regraId, RegraVioladaStatus statusAtual, RegraVioladaStatus statusNovo);

        void AprovarRegrasMarcadas(int processoId);

        void ExcluirRegraPorLoteId(int loteId);

        IList<RegraViolada> ObterRegrasDeCapaOuTermo(int processoId);

        IList<RegraViolada> ObterRegraEmAbertoPorProcesso(int processoId, int regraId, int documentoId);
    }
}