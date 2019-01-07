namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IPalavraReconhecidaRepositorio : IRepositorio<PalavraReconhecida>
    {
        IList<PalavraReconhecida> ObterPorDocumentoId(int documentoId);

        IList<PalavraReconhecida> ObterPorDocumentoIdParaMontarPdf(int documentoId);
    }
}