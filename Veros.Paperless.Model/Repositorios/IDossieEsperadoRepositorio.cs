namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IDossieEsperadoRepositorio : IRepositorio<DossieEsperado>
    {
        IList<DossieEsperado> ObterComCaixaId(int pacoteId);

        DossieEsperado Obter(int caixaId, string numeroContrato, string matriculaAgente);

        IList<DossieEsperado> ObterPorPacote(Pacote pacote);

        IList<DossieEsperado> ObterPorCaixaComLotes(string identificacaoCaixa);

        DossieEsperado ObterDossie(DossieEsperado dossieEsperado);

        DossieEsperado ObterPorMatriculaContratoHipoteca(string matricula, string numeroContrato, string hipoteca);

        int Max();

        DossieEsperado ObterComLotes(int dossieId);

        void ExcluirDossiesPorColeta(int coletaId);

        IList<DossieEsperado> ObterPriorizados(string caixa, string folder, string processo);

        IList<DossieEsperado> ObterRetirados(string caixa, string folder, string processo);

        DossieEsperado ObterDossieDuplicado(DossieEsperado dossieEsperado);

        void AtualizarUltimaOcorrencia(int dossieEsperadoId, Ocorrencia ocorrenciaParaSalvar);
    }
}
