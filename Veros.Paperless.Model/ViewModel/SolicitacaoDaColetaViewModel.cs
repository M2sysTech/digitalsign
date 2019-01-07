namespace Veros.Paperless.Model.ViewModel
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;

    public class SolicitacaoDaColetaViewModel
    {
        public Coleta Coleta
        {
            get; 
            set;
        }

        public int QuantidadeCaixasTipo1
        {
            get;
            set;
        }

        public int QuantidadeCaixasTipo2
        {
            get;
            set;
        }

        public IList<Pacote> PacotesCapturados
        {
            get;
            set;
        }        
    }
}