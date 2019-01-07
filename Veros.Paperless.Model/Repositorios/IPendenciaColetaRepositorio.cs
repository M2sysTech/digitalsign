namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IPendenciaColetaRepositorio : IRepositorio<PendenciaColeta>
    {
        IList<PendenciaColeta> ObterPorArquivo(int arquivoColetaId);

        void CancelarPorArquivo(ArquivoColeta arquivoColeta);
    }
}