namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class ProcessaPaginaPac : IProcessaPaginaPac
    {
        private readonly IOrdenarPaginasPacServico ordenarPaginasPacServico;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IReconhecePaginaPac reconhecePaginaPac;

        public ProcessaPaginaPac(
            IOrdenarPaginasPacServico ordenarPaginasPacServico, 
            IPaginaRepositorio paginaRepositorio, 
            IReconhecePaginaPac reconhecePaginaPac)
        {
            this.ordenarPaginasPacServico = ordenarPaginasPacServico;
            this.paginaRepositorio = paginaRepositorio;
            this.reconhecePaginaPac = reconhecePaginaPac;
        }

        public void Executar(Pagina pagina)
        {
            if (pagina.EhDePac() == false)
            {
                return;
            }

            var paginas = this.paginaRepositorio.ObterPorDocumento(pagina.Documento);

            try
            {
                this.ordenarPaginasPacServico.Executar(paginas);
            }
            catch (System.Exception exception)
            {
                Log.Application.Error(exception);
            }
        }
    }
}