namespace Veros.Paperless.Model.Servicos
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class GravaLogDoDocumentoServico : IGravaLogDoDocumentoServico
    {
        private readonly ILogDocumentoRepositorio logDocumentoRepositorio;
        private readonly ISessaoDoUsuario userSession;

        public GravaLogDoDocumentoServico(
            ILogDocumentoRepositorio logDocumentoRepositorio,
            ISessaoDoUsuario userSession)
        {
            this.logDocumentoRepositorio = logDocumentoRepositorio;
            this.userSession = userSession;
        }

        public void Executar(
            string acaoLogDocumento,
            int documentoId,
            string observacao)
        {
            var logDocumento = new LogDocumento
            {
                Acao = acaoLogDocumento,
                Documento = new Documento { Id = documentoId },
                Usuario = (Usuario)this.userSession.UsuarioAtual ?? new Usuario { Id = -1 },
                Observacao = observacao
            };

            this.logDocumentoRepositorio.Salvar(logDocumento);
        }
    }
}