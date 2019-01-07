namespace Veros.Paperless.Model.ViewModel
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;

    public class DevolucaoColetaViewModel
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

        public int QuantidadeDeCaixasCapturadas(int tipoId)
        {
            var total = 0;

            foreach (var pacote in this.PacotesCapturados)
            {
                total += pacote.Lotes.Count(x => x.TipoDeProcessoId() == tipoId);
            }

            return total;
        }
    }
}