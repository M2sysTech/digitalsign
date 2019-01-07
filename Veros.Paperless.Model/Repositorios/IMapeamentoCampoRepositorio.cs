namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IMapeamentoCampoRepositorio : IRepositorio<MapeamentoCampo>
    {
        IList<MapeamentoCampo> ObterTodosExcetoPac();
     
        IList<MapeamentoCampo> ObterTodosComTipoDocumentoECampo();
        
        bool JaExiste(MapeamentoCampo mapeamentoCampo);

        bool ExisteMapeamentoParaCampo(int campoId);
    }
}