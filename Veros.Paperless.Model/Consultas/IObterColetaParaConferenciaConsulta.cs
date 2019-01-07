namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;
    using ViewModel;

    public interface IObterColetaParaConferenciaConsulta
    {
        IList<ObterColetaParaConferencia> Pesquisar(PesquisaPacoteViewModel filtros);
    }
}
