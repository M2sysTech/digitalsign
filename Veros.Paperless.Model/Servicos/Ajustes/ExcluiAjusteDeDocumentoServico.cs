namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using Repositorios;

    public class ExcluiAjusteDeDocumentoServico : IExcluiAjusteDeDocumentoServico
    {
        private readonly IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio;

        public ExcluiAjusteDeDocumentoServico(IAjusteDeDocumentoRepositorio ajusteDeDocumentoRepositorio)
        {
            this.ajusteDeDocumentoRepositorio = ajusteDeDocumentoRepositorio;
        }

        public void Executar(int ajusteId)
        {
            this.ajusteDeDocumentoRepositorio.ApagarPorId(ajusteId);
        }
    }
}
