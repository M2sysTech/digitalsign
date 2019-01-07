namespace Veros.Paperless.Model.Servicos
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class GravaLogDoProcessoServico : IGravaLogDoProcessoServico
    {
        private readonly ILogProcessoRepositorio logProcessoRepositorio;
        private readonly ISessaoDoUsuario userSession;

        public GravaLogDoProcessoServico(
            ILogProcessoRepositorio logProcessoRepositorio,
            ISessaoDoUsuario userSession)
        {
            this.logProcessoRepositorio = logProcessoRepositorio;
            this.userSession = userSession;
        }

        public void Executar(string acaoLogProcesso,
            Processo processo,
            string observacao)
        {
            var logProcesso = new LogProcesso
            {
                Acao = acaoLogProcesso,
                Processo = processo,
                Usuario = (Usuario)this.userSession.UsuarioAtual,
                Observacao = observacao
            };

            this.logProcessoRepositorio.Salvar(logProcesso);
        }
    }
}