namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    using System.Collections.Generic;
    using ViewModel;

    public interface IObtemControleQualidadeCefViewModelServico
    {
        IList<ProcessamentoQualidadeCefViewModel> Executar(int pacoteProcessadoId = 0);
    }
}
