namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IDossieContingenciaRepositorio : IRepositorio<DossieContingencia>
    {
        IList<DossieContingencia> Pesquisar(string contrato, string matricula, string barcode);        
    }
}