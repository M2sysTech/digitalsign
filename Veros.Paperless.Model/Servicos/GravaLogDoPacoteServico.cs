namespace Veros.Paperless.Model.Servicos
{
    using System;
    using Framework.Servicos;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class GravaLogDoPacoteServico : IGravaLogDoPacoteServico
    {
        private readonly ILogPacoteRepositorio logPacoteRepositorio;
        private readonly ISessaoDoUsuario userSession;

        public GravaLogDoPacoteServico(
            ILogPacoteRepositorio logPacoteRepositorio,
            ISessaoDoUsuario userSession)
        {
            this.logPacoteRepositorio = logPacoteRepositorio;
            this.userSession = userSession;
        }

        public void Executar(string acao, int pacoteId, string observacao)
        {
            var logPacote = new LogPacote
            {
                Acao = acao,
                DataRegistro = DateTime.Now,
                Observacao = observacao,
                Pacote = new Pacote { Id = pacoteId },
                Usuario = (Usuario)this.userSession.UsuarioAtual ?? new Usuario { Id = -1 }
            };

            this.logPacoteRepositorio.Salvar(logPacote);
        }
    }
}