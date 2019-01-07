namespace Veros.Paperless.Model.Servicos.DossiesContingentes
{
    using Entidades;
    using Framework.Modelo;
    using Repositorios;

    public class GeraDossieEsperadoDaContingenciaServico : IGeraDossieEsperadoDaContingenciaServico
    {
        private readonly IDossieContingenciaRepositorio dossieContingenciaRepositorio;
        private readonly IDossieEsperadoRepositorio dossieEsperadoRepositorio;

        public GeraDossieEsperadoDaContingenciaServico(
            IDossieContingenciaRepositorio dossieContingenciaRepositorio, 
            IDossieEsperadoRepositorio dossieEsperadoRepositorio)
        {
            this.dossieContingenciaRepositorio = dossieContingenciaRepositorio;
            this.dossieEsperadoRepositorio = dossieEsperadoRepositorio;
        }

        public DossieEsperado Executar(int dossieContintenteId, int caixaId, string status)
        {
            var dossieContingente = this.dossieContingenciaRepositorio.ObterPorId(dossieContintenteId);

            var dossieEsperado = this.dossieEsperadoRepositorio.Obter(caixaId, dossieContingente.NumeroContrato, dossieContingente.MatriculaAgente);
            
            if (dossieEsperado != null)
            {
                throw new RegraDeNegocioException("O Dossie já está cadastrado para caixa selecionada!");
            }

            dossieEsperado = dossieContingente.ConverteEmDossieEsperado();
            dossieEsperado.PacoteId = caixaId;
            dossieEsperado.Status = status;

            this.dossieEsperadoRepositorio.Salvar(dossieEsperado);

            return dossieEsperado;
        }
    }
}
