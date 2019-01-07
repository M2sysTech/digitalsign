namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;
    using Repositorios;

    public class ReconhecePaginaServico : IReconhecePaginaServico
    {
        private readonly IObterImagemParaReconhecimentoServico obterImagemParaReconhecimentoServico;
        private readonly IProcessaPaginaPac processaPaginaPac;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IReconhecePaginaComum reconhecePaginaComum;

        public ReconhecePaginaServico(
            IObterImagemParaReconhecimentoServico obterImagemParaReconhecimentoServico, 
            IProcessaPaginaPac processaPaginaPac, 
            IPaginaRepositorio paginaRepositorio, 
            IReconhecePaginaComum reconhecePaginaComum)
        {
            this.obterImagemParaReconhecimentoServico = obterImagemParaReconhecimentoServico;
            this.processaPaginaPac = processaPaginaPac;
            this.paginaRepositorio = paginaRepositorio;
            this.reconhecePaginaComum = reconhecePaginaComum;
        }

        public void Executar(Pagina pagina)
        {
            var imagem = this.obterImagemParaReconhecimentoServico.Executar(pagina);
            this.reconhecePaginaComum.Executar(pagina, imagem);
        }
    }
}