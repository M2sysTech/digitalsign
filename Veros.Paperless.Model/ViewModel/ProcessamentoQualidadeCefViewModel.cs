namespace Veros.Paperless.Model.ViewModel
{
    using System;
    using System.Collections.Generic;
    using Entidades;

    public class ProcessamentoQualidadeCefViewModel
    {
        public int LoteCefId { get; set; }

        public string Status { get; set; }

        public DateTime LoteCefData { get; set; }

        public DateTime? DataAprovacao { get; set; }

        public DateTime? DataAssinatura { get; set; }

        public int QuantidadeDeDossies { get; set; }

        public bool PermissaoAprovacao { get; set; }
        
        public IList<DetalheProcessamentoQualidadeCefViewModel> Detalhes
        {
            get;
            set;
        }

        public bool PermiteAprovar()
        {
            if (this.Status == LoteCefStatus.Fechado.Value)
            {
                return true;    
            }

            return false;
        }

        public string StatusDetalhe()
        {
            var statusLoteCef = LoteCefStatus.FromValue(this.Status);

            if (statusLoteCef == LoteCefStatus.Fechado)
            {
                return "Em processamento";
            }

            return statusLoteCef.DisplayName;
        }
    }
}
