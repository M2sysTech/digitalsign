namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface ICamposValidacaoRepositorio : IRepositorio<CamposValidacao>
    {
        IList<CamposValidacao> ObterPorCampoDocumentoComprovacao(Campo campo);
    }
}
