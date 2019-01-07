namespace Veros.Paperless.Model.Servicos
{
    using System.Threading.Tasks;
    using Data;
    using Framework;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class GravaLogDaPaginaServico : IGravaLogDaPaginaServico
    {
        private readonly ILogPaginaRepositorio logPaginaRepositorio;
        private readonly ISessaoDoUsuario userSession;
        private readonly IUnitOfWork unitOfWork;

        public GravaLogDaPaginaServico(
            ILogPaginaRepositorio logPaginaRepositorio,
            ISessaoDoUsuario userSession, 
            IUnitOfWork unitOfWork)
        {
            this.logPaginaRepositorio = logPaginaRepositorio;
            this.userSession = userSession;
            this.unitOfWork = unitOfWork;
        }

        public void Executar(
            string acaoLogPagina,
            int paginaId,
            int documentoId, 
            string observacao)
        {
            var logPagina = new LogPagina
            {
                Acao = acaoLogPagina,
                Pagina = new Pagina { Id = paginaId },
                Documento = new Documento { Id = documentoId },
                Usuario = (Usuario)this.userSession.UsuarioAtual,
                Observacao = observacao
            };

            this.logPaginaRepositorio.Salvar(logPagina);
        }

        public void ExecutarNovaThread(string acaoLogPagina, int paginaId, int documentoId, string observacao)
        {
            Log.Application.Debug("Inicio Log da pagina");
            new TaskFactory().StartNew(() =>
            {
                this.unitOfWork.Transacionar(() =>
                {
                    try
                    {
                        var logPagina = new LogPagina
                        {
                            Acao = acaoLogPagina,
                            Pagina = new Pagina { Id = paginaId },
                            Documento = new Documento { Id = documentoId },
                            Usuario = (Usuario)this.userSession.UsuarioAtual,
                            Observacao = observacao
                        };

                        this.logPaginaRepositorio.Salvar(logPagina);
                        Log.Application.Debug("Log da pagina salvo");
                    }
                    catch (System.Exception exception)
                    {
                        Log.Application.Error(exception);
                    }
                });
            }).Wait();
        }
    }
}