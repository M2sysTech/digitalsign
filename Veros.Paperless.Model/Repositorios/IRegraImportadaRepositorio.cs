namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IRegraImportadaRepositorio : IRepositorio<RegraImportada>
    {
        IList<RegraImportada> ObterRegrasImportadasPorDocumento(int documentoId);

        IList<RegraImportada> ObterSomenteDesvinculadasPorProcesso(int processoId);

        Documento ObterDocumentoPorVinculoEProcesso(string vinculo, int processoId);
    }
}
