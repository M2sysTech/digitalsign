namespace Veros.Paperless.Model.Servicos
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class GravaLogDoPacoteProcessadoServico : IGravaLogDoPacoteProcessadoServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly ILogPacoteProcessadoRepositorio logPacoteProcessadoRepositorio;

        public GravaLogDoPacoteProcessadoServico(
            ISessaoDoUsuario userSession, 
            ILogPacoteProcessadoRepositorio logPacoteProcessadoRepositorio)
        {
            this.userSession = userSession;
            this.logPacoteProcessadoRepositorio = logPacoteProcessadoRepositorio;
        }

        public void Executar(
            string acao,
            int pacoteProcessadoId,
            string observacao)
        {
            var log = new LogPacoteProcessado()
            {
                Acao = acao,
                PacoteProcessado = new PacoteProcessado { Id = pacoteProcessadoId },
                Usuario = (Usuario)this.userSession.UsuarioAtual,
                Observacao = observacao
            };

            this.logPacoteProcessadoRepositorio.Salvar(log);
        }
    }
}