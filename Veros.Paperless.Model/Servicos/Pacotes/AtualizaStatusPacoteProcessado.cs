namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using Entidades;
    using Repositorios;

    public class AtualizaStatusPacoteProcessado : IAtualizaStatusPacoteProcessado
    {
        private readonly IPacoteProcessadoRepositorio pacoteProcessadoRepositorio;

        public AtualizaStatusPacoteProcessado(IPacoteProcessadoRepositorio pacoteProcessadoRepositorio)
        {
            this.pacoteProcessadoRepositorio = pacoteProcessadoRepositorio;
        }

        public void Executar(PacoteProcessado rajada)
        {
            this.pacoteProcessadoRepositorio.AtualizarStatus(rajada.Id, StatusPacote.Pendente);
        }
    }
}