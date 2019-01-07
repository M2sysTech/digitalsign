namespace Veros.Paperless.Model.Servicos.ArquivosDeColeta
{
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class GravaArquivoDeColetaServico : IGravaArquivoDeColetaServico
    {
        private readonly IArquivoColetaRepositorio arquivoColetaRepositorio;
        private readonly IPendenciaColetaRepositorio pendenciaColetaRepositorio;

        public GravaArquivoDeColetaServico(IArquivoColetaRepositorio arquivoColetaRepositorio, 
            IPendenciaColetaRepositorio pendenciaColetaRepositorio)
        {
            this.arquivoColetaRepositorio = arquivoColetaRepositorio;
            this.pendenciaColetaRepositorio = pendenciaColetaRepositorio;
        }

        public void Executar(ArquivoColeta arquivoColeta)
        {
            foreach (var pendencia in arquivoColeta.Pendencias)
            {
                this.pendenciaColetaRepositorio.Salvar(pendencia);
            }

            if (arquivoColeta.Pendencias.Any(x => string.IsNullOrEmpty(x.SubTipo)) == false)
            {
                arquivoColeta.Status = ArquivoColeta.SemPendencias;
            }

            this.arquivoColetaRepositorio.Salvar(arquivoColeta);
        }
    }
}
