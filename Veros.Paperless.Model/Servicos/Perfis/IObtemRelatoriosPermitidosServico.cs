namespace Veros.Paperless.Model.Servicos.Perfis
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public interface IObtemRelatoriosPermitidosServico
    {
        IList<Funcionalidade> Executar();
    }
}