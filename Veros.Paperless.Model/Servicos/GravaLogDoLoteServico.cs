namespace Veros.Paperless.Model.Servicos
{
    using Framework.Servicos;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class GravaLogDoLoteServico : IGravaLogDoLoteServico
    {
        private readonly ILogLoteRepositorio logLoteRepositorio;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly ISessaoDoUsuario sessaoDoUsuario;

        public GravaLogDoLoteServico(
            ILogLoteRepositorio logLoteRepositorio,
            IUsuarioRepositorio usuarioRepositorio,
            ISessaoDoUsuario sessaoDoUsuario)
        {
            this.logLoteRepositorio = logLoteRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
            this.sessaoDoUsuario = sessaoDoUsuario;
        }

        public void Executar(string acaoLogProcesso, int loteId, string observacao, string token)
        {
            var usuario = this.usuarioRepositorio.ObterPorToken(token);

            var logLote = new LogLote
            {
                Acao = acaoLogProcesso,
                Lote = new Lote { Id = loteId },
                Usuario = usuario,
                Observacao = observacao
            };

            this.logLoteRepositorio.Salvar(logLote);
        }

        public void Executar(string acaoLogLote, int loteId, string observacao)
        {
            var logLote = new LogLote
            {
                Acao = acaoLogLote,
                Lote = new Lote { Id = loteId },
                Usuario = (Usuario)this.sessaoDoUsuario.UsuarioAtual ?? new Usuario { Id = -1 },
                Observacao = observacao
            };

            this.logLoteRepositorio.Salvar(logLote);
        }
    }
}