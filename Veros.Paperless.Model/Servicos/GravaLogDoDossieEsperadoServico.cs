namespace Veros.Paperless.Model.Servicos
{
    using System;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class GravaLogDoDossieEsperadoServico : IGravaLogDoDossieEsperadoServico
    {
        private readonly ILogDossieEsperadoRepositorio logDossieEsperadoRepositorio;
        private readonly ISessaoDoUsuario userSession;

        public GravaLogDoDossieEsperadoServico(
            ILogDossieEsperadoRepositorio logDossieEsperadoRepositorio,
            ISessaoDoUsuario userSession)
        {
            this.logDossieEsperadoRepositorio = logDossieEsperadoRepositorio;
            this.userSession = userSession;
        }

        public void Executar(string acaoLogDossieEsperado,
            DossieEsperado dossieEsperado,
            string observacao)
        {
            var logDossieEsperado = new LogDossieEsperado
            {
                Acao = acaoLogDossieEsperado,
                DataRegistro = DateTime.Now,
                Usuario = (Usuario)this.userSession.UsuarioAtual,
                DossieEsperado = dossieEsperado,
                Observacao = observacao
            };

            this.logDossieEsperadoRepositorio.Salvar(logDossieEsperado);
        }
    }
}