namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Framework.Modelo;
    using Veros.Paperless.Model.Entidades;

    public interface IValorReconhecidoRepositorio : IRepositorio<ValorReconhecido>
    {
        IList<ValorReconhecido> ObtemPorDocumento(int documentoId);

        ValorReconhecido ObtemPrimeiroPorPagina(int paginaId);
    }
}