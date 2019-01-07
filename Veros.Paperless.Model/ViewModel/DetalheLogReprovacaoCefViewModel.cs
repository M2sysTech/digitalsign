namespace Veros.Paperless.Model.ViewModel
{
    using System.Collections.Generic;

    public class DetalheLogReprovacaoCefViewModel
    {
        public IList<LogDeReprovacaoCefViewModel> Logs { get; set; }

        public IList<RegraVioladaReprovacaoCefViewModel> Regras { get; set; }
    }
}