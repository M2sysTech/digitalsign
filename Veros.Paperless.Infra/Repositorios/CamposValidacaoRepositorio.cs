namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class CamposValidacaoRepositorio : Repositorio<CamposValidacao>, ICamposValidacaoRepositorio
    {
        public IList<CamposValidacao> ObterPorCampoDocumentoComprovacao(Campo campo)
        {
            return this.Session.QueryOver<CamposValidacao>()
                .Where(x => x.CampoDocumentoComprovacao == campo)
                .List<CamposValidacao>();
        }
    }
}
