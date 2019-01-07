namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IOcorrenciaRepositorio : IRepositorio<Ocorrencia>
    {
        void AlterarStatus(int ocorrenciaId, OcorrenciaStatus statusNovo);

        IList<Ocorrencia> Pesquisar(IList<OcorrenciaStatus> status, string barcodeCaixa, int tipoOcorrenciaId);

        Ocorrencia ObterPorIdComDocumento(int ocorrenciaId);

        IList<Ocorrencia> ObterPorColeta(int coletaId);

        void LimparResponsavel(int ocorrenciaId);

        IList<Ocorrencia> ObterPorDossie(int dossieId);

        Ocorrencia ObterComTipo(int ocorrenciaId);
    }
}